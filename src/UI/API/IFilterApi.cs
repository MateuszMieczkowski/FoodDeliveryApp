using Refit;
using UI.API.Responses;

namespace UI.API
{
	public interface IFilterApi
	{
		[Get("restaurants/categories")]
		Task<RestaurantCategory> GetRestaurantCategories();

		[Get("restaurants/cities")]
		Task<RestaurantCategory> GetCities();
	}
}
