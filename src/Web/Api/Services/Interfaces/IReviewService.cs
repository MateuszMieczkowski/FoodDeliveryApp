using Web.Api.Models;
using Web.Api.Models.RestaurantDtos;

namespace Web.Api.Services.Interfaces;

public interface IRestaurantReviewService
{
    Task<PagedResult<RestaurantReviewDto>> GetReviewsAsync(int restaurantId, int pageNumber, int pageSize);

    Task AddReviewAsync(int restaurantId, RestaurantReviewForUpdateDto reviewDto);

    Task DeleteReviewAsync(int restaurantId, int reviewId);

}
