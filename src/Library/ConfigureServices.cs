﻿using Library.Authorization;
using Library.Repositories.Interfaces;
using Library.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Library.DataPersistence;
using Library.Services;
using Library.Services.DiscountCalculator;
using Library.Services.Interfaces;
using Library.Services.ShoppingCart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Library.Services.OrderCostCalculator;

namespace Library;

public static class ConfigureServices
{
    public static void AddLibraryServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IRestaurantCategoryRepository, RestaurantCategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IShoppingCartItemRepository, ShoppingCartItemRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
      
        
        services.AddScoped<IShoppingCartService, ShoppingCartService>();
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IRestaurantReviewService, RestaurantReviewService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthorizationHandler, RestaurantManagerRequirementHandler>();
        services.AddScoped<IAuthorizationHandler, AccountOwnerRequirementHandler>();
        services.AddScoped<IUserContextAccessor, UserContextAccessor>();
        services.AddScoped<IRequirementService, RequirementService>();

        services.AddTransient<IDiscountCalculator, PercentageDiscountCalculator>();
        services.AddTransient<IDiscountCalculator, AmountDiscountCalculator>();
        services.AddTransient<IDiscountCalculatorFactory, DiscountCalculatorFactory>();
        services.AddTransient<IOrderCostCalculator, OrderCostCalculator>();
        
        services.AddAuthenticationAndAuthorization(configuration);
        
        services.AddHostedService<BackgroundUpdateRestaurantsRatingService>();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration["ConnectionStrings:AzureDb"]);
        });

    }
}
