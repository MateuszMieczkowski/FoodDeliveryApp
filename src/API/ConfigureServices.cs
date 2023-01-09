using API.Authorization;
using API.Middlewares;
using API.Services;
using API.Services.Interfaces;
using API.Services.ShoppingCart;
using Microsoft.AspNetCore.Authorization;

namespace API;

internal static class ConfigureServices
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IShoppingCartService, ShoppingCartService>();
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IRestaurantReviewService, RestaurantReviewService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthorizationHandler, RestaurantManagerRequirementHandler>();
        services.AddScoped<IUserContextAccessor, UserContextAccessor>();
        services.AddControllers(options =>
        {
            options.ReturnHttpNotAcceptable = true;
        }).AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });
        services.AddDistributedMemoryCache();
        services.AddSession();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddHttpContextAccessor();

        services.AddTransient<ExceptionHandlingMiddleware>();
    }
}
