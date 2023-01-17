using Library.Models;
using Library.Models.RestaurantDtos;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("/api/restaurants/{restaurantId:int}/reviews")]
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
    [Authorize(Roles = "user")]
    public async Task<ActionResult> AddReview([FromRoute] int restaurantId, [FromBody] RestaurantReviewForUpdateDto dto)
    {
        await _restaurantReviewService.AddReviewAsync(restaurantId, dto);
        return Ok();
    }
    [HttpDelete("{reviewId:int}")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> DeleteReview([FromRoute] int restaurantId, [FromRoute] int reviewId)
    {
        await _restaurantReviewService.DeleteReviewAsync(restaurantId, reviewId);
        return Ok();
    }


}


