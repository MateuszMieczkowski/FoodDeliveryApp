using Library.Models;
using Library.Models.RestaurantDtos;

namespace Library.Services.Interfaces;

public interface IRestaurantReviewService
{
    Task<PagedResult<RestaurantReviewDto>> GetReviewsAsync(int restaurantId, int pageNumber, int pageSize);

    Task AddReviewAsync(int restaurantId, RestaurantReviewForUpdateDto reviewDto);

    Task DeleteReviewAsync(int restaurantId, int reviewId);
}