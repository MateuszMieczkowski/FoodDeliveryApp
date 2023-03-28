using Library.Models.ShoppingCartDtos;

namespace Library.Services.Interfaces;

public interface IShoppingCartService
{
    Task AddToCartAsync(int productId);

    Task DeleteFromCartAsync(int productId);

    ShoppingCartDto GetShoppingCart(int restaurantId);
}