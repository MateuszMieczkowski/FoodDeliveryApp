using API.Models.ShoppingCartDtos;

namespace API.Services.Interfaces;

public interface IShoppingCartService
{
    Task AddToCartAsync(int productId);

    Task DeleteFromCartAsync(int productId);

    ShoppingCartDto GetShoppingCart();
}