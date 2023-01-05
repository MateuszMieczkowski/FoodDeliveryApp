using API.Models;
using API.Models.RestaurantDtos;

namespace API.Services.Interfaces;

public interface IRestaurantReviewService
{
    Task<PagedResult<RestaurantReviewDto>> GetReviewsAsync(int restaurantId, int pageNumber, int pageSize);

    Task AddReviewAsync(int restaurantId, RestaurantReviewForUpdateDto reviewDto);

    Task DeleteReviewAsync(int restaurantId, int reviewId);

}
