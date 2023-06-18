using Refit;
using UI.API.Responses;

namespace UI.API;

public interface IShoppingCartApi
{
	[Get("/shoppingCart")]
	Task<ShoppingCart> GetCartAsync([Query] int restaurantId, [Query] Guid shoppingCartId);

	[Post("/shoppingCart")]
	Task AddToCartAsync([Query] int productId, [Query] Guid shoppingCartId);

	[Delete("/shoppingCart")]
	Task DeleteFromCartAsync([Query] int productId, [Query] Guid shoppingCartId);
}
