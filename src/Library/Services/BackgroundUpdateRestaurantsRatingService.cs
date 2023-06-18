using Library.DataPersistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Library.Services;

public class BackgroundUpdateRestaurantsRatingService : BackgroundService
{
    private readonly ILogger<BackgroundUpdateRestaurantsRatingService> _logger;
    private readonly IServiceProvider _serviceProvider;
	private readonly TimeSpan _updateInterval = new(1, 0, 0);

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

    private async Task UpdateRatings(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await dbContext.Database.ExecuteSqlRawAsync(
                    @"UPDATE Restaurants
                        SET Rating = (SELECT  ISNULL(AVG(CAST(RestaurantReviews.Rating as float)), 0.0)
                                      FROM RestaurantReviews
                                      WHERE RestaurantId = Restaurants.Id);", cancellationToken: stoppingToken);
                await dbContext.SaveChangesAsync(stoppingToken);
            }
            _logger.LogInformation("BackgroundUpdateRestaurantsRatingService: updated ratings on restaurant");
            await Task.Delay(_updateInterval, stoppingToken);
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation(
            "BackgroundUpdateRestaurantsRatingService is stopping.");

        await base.StopAsync(stoppingToken);
    }
}