using Refit;
using UI.API.Responses;

namespace UI.API;

public interface IRestaurantApi
{
    [Get("/restaurants")]
    Task <PagedResult<Restaurant>> GetRestaurants(string? name, string? city, string? category, string? searchQuery, int pageNumber, int pageSize);
}