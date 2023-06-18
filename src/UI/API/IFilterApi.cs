using Refit;
using UI.API.Responses;

namespace UI.API
{
	public interface IFilterApi
	{
		[Get("/restaurants/categories")]
        Task<List<RestaurantCategory>> GetRestaurantCategories();

		[Get("/restaurants/cities")]
		Task<List<string>> GetCities();
	}
}
