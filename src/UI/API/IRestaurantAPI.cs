using Refit;
using UI.API.Responses;

namespace UI.API;

public interface IRestaurantApi
{
    [Get("/restaurants")]
    Task <PagedResult<Restaurant>> GetRestaurants(string? name = null, string? city = null, string? category = null, string? searchQuery = null, int pageNumber = 1 , int pageSize= 10);

    [Get("/restaurants/{id}")]
    Task<Restaurant> GetRestaurant(int id);
}