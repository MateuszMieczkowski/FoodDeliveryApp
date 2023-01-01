using Library.DataPersistence;
using Library.Entities;
using Library.Services.ShoppingCart;
using Microsoft.AspNetCore.Identity;
using Web.Api.Services;
using Web.Api.Services.Interfaces;
using Web.Middlewares;

namespace Web;

internal static class ConfigureServices
{
    public static void AddWebServices(this IServiceCollection services)
    {
        services.AddScoped<IShoppingCartService, ShoppingCartService>();
        services.AddScoped<IRestaurantService, RestaurantService>();
        services.AddScoped<IRestaurantReviewService, RestaurantReviewService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();

        services.AddAuthentication();
        services.AddIdentity<User, IdentityRole<Guid>>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.User.RequireUniqueEmail = true;
        })
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();

        services.AddRazorPages().AddRazorPagesOptions(options =>
        {
            options.Conventions.AddPageRoute("/restaurant/start", "/");
        });

        services.AddControllers(options =>
        {
            
            options.ReturnHttpNotAcceptable = true;
        }).AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });
        services.AddDistributedMemoryCache();
        services.AddSession();
        services.AddSwaggerGen();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddHttpContextAccessor();

        services.AddTransient<ExceptionHandlingMiddleware>();
    }
}
