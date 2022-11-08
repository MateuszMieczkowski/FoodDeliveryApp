using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Models.RestaurantDtos;

namespace Web.Api.Controllers;

[ApiController]
[Route("/api/restaurant/{restaurantId}/review")]
public class ReviewsController : ControllerBase
{
    private readonly IRestaurantRepository _restaurantRepository;

    public ReviewsController(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantReviewDto>>> GetReviews(int restaurantId)
    {
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
        if(restaurant is null)
        {
            return NotFound();
        }
        var reviews = restaurant.Reviews;
        return Ok(reviews);
    }

    [HttpPost]
    public async Task<ActionResult> AddReview(int restaurantId, RestaurantReviewForUpdateDto reviewDto)
    {
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
        if (restaurant is null)
        {
            return NotFound();
        }
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }
        var newReview = new RestaurantReview()
        {
            Title = reviewDto.Title,
            Description = reviewDto.Description,
            Rating = reviewDto.Rating
        };
        restaurant.Reviews?.Add(newReview);
        await _restaurantRepository.SaveChangesAsync();

        return Ok();
    }


}


