﻿using API.Middlewares;

namespace API;

internal static class ConfigureServices
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.AllowEmptyInputInBodyModelBinding = true;
            options.ReturnHttpNotAcceptable = true;
        }).AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });
        services.AddDistributedMemoryCache();
        services.AddSession();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddHttpContextAccessor();
        services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin()));
        services.AddTransient<ExceptionHandlingMiddleware>();
    }
}
