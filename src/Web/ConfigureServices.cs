using System.Runtime.CompilerServices;

namespace Web;

internal static class ConfigureServices
{
    public static void AddWebServices(this IServiceCollection services)
    {
        services.AddRazorPages();

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
    }
}
