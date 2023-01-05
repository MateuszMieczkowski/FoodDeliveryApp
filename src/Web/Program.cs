using Library;
using Library.DataPersistence;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Web;
using Web.Api.Models.ProductDtos;
using Web.Api.Services.Interfaces;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information().Enrich
    .FromLogContext().WriteTo
    .Console()
    .CreateLogger();
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);
builder.Services.AddWebServices();
builder.Services.AddLibraryServices(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints => endpoints.MapControllers());

//TODO: add shoppingCartController
app.MapPost("api/shoppingCart", async ([FromQuery] int productId, IShoppingCartService cartService, ApplicationDbContext dbContext) =>
{
    await cartService.AddToCartAsync(productId);
    return Results.NoContent();
});

app.MapGet("api/shoppingCart", (IShoppingCartService shoppingCartService) =>
{
    var json = JsonConvert.SerializeObject(shoppingCartService.GetShoppingCart(), Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
    return json;
});


app.MapRazorPages();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var dbSeeder = new DbSeeder(context);
    dbSeeder.Seed();
}
app.Run();
