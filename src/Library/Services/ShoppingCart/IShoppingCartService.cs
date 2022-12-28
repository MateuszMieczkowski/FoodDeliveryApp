using Library.Entities;

namespace Library.Services.ShoppingCart;

public interface IShoppingCartService
{
    ShoppingCart ShoppingCart { get; set; }
    Task AddToCartAsync(Product product);
    Task DeleteFromCartAsync(Product product);
}
