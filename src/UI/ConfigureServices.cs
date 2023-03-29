using Refit;
using UI.API;
using UI.Settings;

namespace UI
{
	public static class ConfigureServices
	{
		public static IServiceCollection AddUIServices(this IServiceCollection services, IConfiguration configuration)
		{

			var apiSettings = new ApiSettings();
			configuration.GetSection("APISettings").Bind(apiSettings);
			services.AddSingleton(apiSettings);

			services.AddRefitClient<IRestaurantApi>()
				.ConfigureHttpClient(client => client.BaseAddress = new Uri(apiSettings.BaseURL));

			services.AddRefitClient<IFilterAPI>()
				.ConfigureHttpClient(client => client.BaseAddress = new Uri(apiSettings.BaseURL));

			services.AddRefitClient<IProductAPI>()
				.ConfigureHttpClient(client => client.BaseAddress = new Uri(apiSettings.BaseURL));

			services.AddRefitClient<IShoppingCartAPI>()
				.ConfigureHttpClient(client => client.BaseAddress = new Uri(apiSettings.BaseURL));
			return services;
		}
	}
}
