using Library.Repositories.Interfaces;
using Library.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Library.DataPersistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Library;

public static class ConfigureServices
{
    public static void AddLibraryServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IRestaurantCategoryRepository, RestaurantCategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);
        });
    }
}
