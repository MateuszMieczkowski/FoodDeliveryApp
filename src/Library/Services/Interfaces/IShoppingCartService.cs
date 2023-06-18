using Library.Models.ShoppingCartDtos;

namespace Library.Services.Interfaces;

public interface IShoppingCartService
{

    Task AddToCartAsync(int productId, Guid shoppingCartId);

    Task DeleteFromCartAsync(int productId, Guid shoppingCartId);

    Task<ShoppingCartDto> GetShoppingCartAsync(int restaurantId, Guid shoppingCartId);
}