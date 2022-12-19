using System.Runtime.CompilerServices;
using Web.Middlewares;

namespace Web;

internal static class ConfigureServices
{
    public static void AddWebServices(this IServiceCollection services)
    {
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
