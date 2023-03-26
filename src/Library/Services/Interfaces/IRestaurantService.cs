using Library.Models;
using Library.Models.RestaurantDtos;

namespace Library.Services.Interfaces;

public interface IRestaurantService
{
    Task<PagedResult<RestaurantDto>> GetRestaurantsAsync
        (string? name, string? city, string? category, string? searchQuery, int pageNumber, int pageSize);

    Task<RestaurantDto> GetRestaurantAsync(int restaurantId);

    Task DeleteRestaurantAsync(int restaurantId);

    Task<RestaurantDto> CreateRestaurantAsync(RestaurantForUpdateDto dto);

    Task UpdateRestaurantAsync(int restaurantId, RestaurantForUpdateDto dto);

    IEnumerable<RestaurantCategoryDto> GetRestaurantCategories();

    Task<List<string>> GetCities();
}