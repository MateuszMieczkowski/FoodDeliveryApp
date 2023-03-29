using Refit;
using UI.API.Responses;

namespace UI.API;

public interface IShoppingCartAPI
{
	[Get("/shoppingCart/{restaurantId}")]
	Task<ShoppingCart> GetCartAsync(int restaurantId);

	[Post("/shoppingCart")]
	Task AddToCartAsync(int productId);

	[Delete("/shoppingCart")]
	Task DeleteFromCartAsync(int productId);
}
