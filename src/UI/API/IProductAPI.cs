using Refit;
using UI.API.Responses;

namespace UI.API
{
	public interface IProductAPI
	{
		[Get("/restaurants/{restaurantId}/products")]
		Task<List<Product>> GetProducts(int restaurantId);
	}
}
