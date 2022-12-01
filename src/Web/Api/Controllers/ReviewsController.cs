using AutoMapper;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Models.RestaurantDtos;

namespace Web.Api.Controllers;

[ApiController]
[Route("/api/restaurants/{restaurantId}/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IMapper _mapper;

    public ReviewsController(IRestaurantRepository restaurantRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantReviewDto>>> GetReviews(int restaurantId)
    {
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
        if(restaurant is null)
        {
            return NotFound();
        }
        var reviews = _mapper.Map<IEnumerable<RestaurantReviewDto>>(restaurant.Reviews);
        return Ok(reviews);
    }

    [HttpPost]
    public async Task<IActionResult> AddReview(int restaurantId, RestaurantReviewForUpdateDto reviewDto)
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
        var newReview = _mapper.Map<RestaurantReview>(reviewDto);
        restaurant.Reviews?.Add(newReview);
        await _restaurantRepository.SaveChangesAsync();

        return Ok();
    }
    [HttpDelete("{reviewId}")]
    public async Task<IActionResult> DeleteReview(int restaurantId, int reviewId)
    {
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
        if (restaurant is null)
        {
            return NotFound();
        }

        var review = restaurant.Reviews?.FirstOrDefault(r => r.Id == reviewId);
        if (review is null)
        {
            return NotFound();
        }

        restaurant.Reviews.Remove(review);
        await _restaurantRepository.SaveChangesAsync();

        return Ok();
    }


}


