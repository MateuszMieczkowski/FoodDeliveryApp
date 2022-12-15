using Library.DataPersistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Web.Services
{
    public class BackgroundUpdateRestaurantsRatingService : BackgroundService
    {
        private readonly ILogger<BackgroundUpdateRestaurantsRatingService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _updateInterval = new TimeSpan(1,0,0);

        public BackgroundUpdateRestaurantsRatingService(ILogger<BackgroundUpdateRestaurantsRatingService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("BackgroundUpdateRestaurantsRatingService has started");
            await UpdateRatings(stoppingToken);
        }

        private async Task UpdateRatings(CancellationToken stopingToken)
        {
            while(!stopingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    await dbContext.Restaurants.Include(r => r.Reviews).ForEachAsync(r =>
                    {
                        var rating = 0.0;

                        if (r.Reviews!.Any())
                        {
                            rating = r.Reviews!.Average(r => r.Rating);
                        }
                        r.Rating = rating;

                    }, cancellationToken: stopingToken);
                    await dbContext.SaveChangesAsync();
                }
                _logger.LogInformation("BackgroundUpdateRestaurantsRatingService: updated ratings on restaurant");
                await Task.Delay(_updateInterval, stopingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "BackgroundUpdateRestaurantsRatingService is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
