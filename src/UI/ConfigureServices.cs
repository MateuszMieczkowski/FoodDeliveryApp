using Refit;
using System.Reflection;
using UI.API;
using UI.Services;
using UI.Services.LocalStorage;
using UI.Services.ShoppingCart;
using UI.Services.StateContainer;
using UI.Services.UserService;
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

			services.AddSingleton<ILocalStorageAccessor, LocalStorageAccessor>();
			services.AddSingleton<IUserService, UserService>();
			services.AddSingleton<IShoppingCartService, ShoppingCartService>();
			services.AddSingleton<StateContainer>();
            services.AddSingleton<AuthHeaderHandler>();
            services.RegisterRefitClients(apiSettings);
            return services;
		}

		public static IServiceCollection RegisterRefitClients(this IServiceCollection services, ApiSettings apiSettings)
		{
			var assembly = Assembly.GetExecutingAssembly();
			var refitTypesInfo = assembly.DefinedTypes
				.Where(x => x.Name.EndsWith("Api")  && x.Namespace == "UI.API");

			var refitSettings = new RefitSettings()
			{

			};
			foreach (var refitTypeInfo  in refitTypesInfo)
			{
				var type = refitTypeInfo.AsType();
				services.AddRefitClient(type)
					.ConfigureHttpClient(client => client.BaseAddress = new Uri(apiSettings.BaseURL));
            }
			return services;
        }
	}
}
