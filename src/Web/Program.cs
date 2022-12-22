using Library;
using Library.DataPersistence;
using Library.Services.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Web;
using Web.Api.Models.ProductDtos;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information().Enrich
    .FromLogContext().WriteTo
    .Console()
    .CreateLogger();
builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddWebServices();
builder.Services.AddLibraryServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("https://localhost:7007/swagger/v1/swagger.json", "v1");
    });
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.UseEndpoints(endpoints => endpoints.MapControllers());

//TODO: add shoppingCartController
app.MapPost("api/shoppingCart", async ([FromQuery] int productId, ShoppingCartService cartService, ApplicationDbContext dbContext) =>
{
   var product =  await dbContext.Products.FindAsync(productId);
    if (product is null)
    {
        return Results.NotFound();
    }
    await cartService.AddToCartAsync(product);
    return Results.NoContent();
});

app.MapGet("api/shoppingCart", (ShoppingCartService shoppingCartService) =>
{
    var json = JsonConvert.SerializeObject(shoppingCartService.ShoppingCart.ShoppingCartItems,Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
    return Results.Json(json);
});


app.MapRazorPages();


using(var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var dbSeeder = new DbSeeder(context);
    dbSeeder.Seed();
}
app.Run();
