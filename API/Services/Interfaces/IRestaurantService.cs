using Web.Api.Models.RestaurantDtos;
using Web.Api.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Web.Api.Services.Interfaces
{
    public interface IRestaurantService
    {
        Task<PagedResult<RestaurantDto>> GetRestaurantsAsync
        (string? name, string? city, string? category, string? searchQuery, int pageNumber, int pageSize);

        Task<RestaurantDto> GetRestaurantAsync(int restaurantId);

        Task DeleteRestaurantAsync(int restaurantId);

        Task<RestaurantDto> CreateRestaurantAsync(RestaurantForUpdateDto dto);

        Task UpdateRestaurantAsync(int restaurantId, RestaurantForUpdateDto dto);

        IEnumerable<RestaurantCategoryDto> GetRestaurantCategories();
    }
}