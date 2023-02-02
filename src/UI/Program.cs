using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Refit;
using UI;
using UI.API;
using UI.Settings;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

var apiSettings = new ApiSettings();
builder.Configuration.GetSection("APISettings").Bind(apiSettings);
builder.Services.AddSingleton(apiSettings);

builder.Services.AddRefitClient<IRestaurantApi>()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri(apiSettings.BaseURL));

await builder.Build().RunAsync();
