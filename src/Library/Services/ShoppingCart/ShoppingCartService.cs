using Library.DataPersistence;
using Library.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Services.ShoppingCart;

public class ShoppingCartService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IServiceProvider _serviceProvider;

    public ShoppingCart ShoppingCart { get; set; }

    public ShoppingCartService(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
    {
        _dbContext = dbContext;
        _serviceProvider = serviceProvider;
        ShoppingCart = GetShoppingCart();
    }

    private ShoppingCart GetShoppingCart()
    {
        var httpContext = _serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;

        string? shoppingCartIdFromSession = httpContext?.Session.GetString("shoppingCartId");
        Guid shoppingCartId;
        if(!Guid.TryParse(shoppingCartIdFromSession, out shoppingCartId))
        {
            shoppingCartId = Guid.NewGuid();
        }

        httpContext?.Session.SetString("shoppingCartId", shoppingCartId.ToString());

        var shoppingCartItems = _dbContext.ShoppingCartItems.Include(r => r.Product)
            .Where(r => r.ShoppingCartId.Equals(shoppingCartId))?.ToList() ?? new List<ShoppingCartItem>();

        return new ShoppingCart() { ShoppingCartId = shoppingCartId, ShoppingCartItems = shoppingCartItems };
        
    }

    public async Task AddToCartAsync(Product product)
    {
        var shoppingCartItem =  ShoppingCart!.ShoppingCartItems!.SingleOrDefault(r => r.Id == product.Id);
        if(shoppingCartItem is null)
        {
            var newItem = new ShoppingCartItem()
            {
                Product = product,
                ShoppingCartId = ShoppingCart.ShoppingCartId,
                Quantity = 1
            };
            await _dbContext.ShoppingCartItems.AddAsync(newItem);
            await _dbContext.SaveChangesAsync();
            return;
        }
        shoppingCartItem.Quantity++;
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteFromCartAsync(Product product)
    {
        var shoppingCartItem = ShoppingCart!.ShoppingCartItems!.SingleOrDefault(r => r.Id == product.Id);
        if (shoppingCartItem is null)
        {
            return;
        }
        if(shoppingCartItem.Quantity == 1)
        {
            _dbContext.ShoppingCartItems.Remove(shoppingCartItem);
            await _dbContext.SaveChangesAsync();
            return;
        }
        shoppingCartItem.Quantity--;
        await _dbContext.SaveChangesAsync();
    }
}
