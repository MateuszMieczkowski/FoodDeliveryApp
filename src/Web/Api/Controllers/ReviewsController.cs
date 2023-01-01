using Microsoft.AspNetCore.Mvc;
using Web.Api.Models;
using Web.Api.Models.RestaurantDtos;
using Web.Api.Services.Interfaces;

namespace Web.Api.Controllers;

[ApiController]
[Route("/api/restaurants/{restaurantId}/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IRestaurantReviewService _restaurantReviewService;

    public ReviewsController(IRestaurantReviewService restaurantReviewService)
    {
        _restaurantReviewService = restaurantReviewService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<RestaurantReviewDto>>> GetReviews(int restaurantId, int pageNumber = 1, int pageSize = 10)
    {
        var result = await _restaurantReviewService.GetReviewsAsync(restaurantId, pageNumber, pageSize);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddReview(int restaurantId, RestaurantReviewForUpdateDto dto)
    {
        await _restaurantReviewService.AddReviewAsync(restaurantId, dto);
        return Ok();
    }
    [HttpDelete("{reviewId}")]
    public async Task<IActionResult> DeleteReview(int restaurantId, int reviewId)
    {
        await _restaurantReviewService.DeleteReviewAsync(restaurantId, reviewId);
        return Ok();
    }


}


