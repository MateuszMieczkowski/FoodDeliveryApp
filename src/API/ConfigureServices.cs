using API.Middlewares;
using Microsoft.OpenApi.Models;

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
		services.AddSwaggerGen(options =>
		{

			options.SwaggerDoc("v1", new OpenApiInfo
			{
				Version = "v1",
				Title = "FoodDeliveryApp API",
				Description =
					"An ASP.NET Core Web API for managing restaurants, products, orders etc."
			});
			options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Description = "Please enter a valid token",
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				BearerFormat = "JWT",
				Scheme = "Bearer"
			});
			options.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] {}
					}
				});
		});

		services.AddDistributedMemoryCache();
		services.AddSession();
		services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		services.AddHttpContextAccessor();
		services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("https://localhost:7287").AllowAnyHeader()));
		services.AddTransient<ExceptionHandlingMiddleware>();
	}
}
