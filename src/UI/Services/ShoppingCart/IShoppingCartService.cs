using UI.API.Responses;

namespace UI.Services.ShoppingCart;

public interface IShoppingCartService
{
    Task Add(int productId);

    Task Delete(int productId);

    Task<API.Responses.ShoppingCart> GetCartAsync(int restaurantId);
}
