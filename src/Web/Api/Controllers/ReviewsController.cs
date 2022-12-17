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
    private readonly IReviewRepository _reviewRepository;
    private readonly IMapper _mapper;

    public ReviewsController(IRestaurantRepository restaurantRepository, IMapper mapper, IReviewRepository reviewRepository)
    {
        _restaurantRepository = restaurantRepository;
        _mapper = mapper;
        _reviewRepository = reviewRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantReviewDto>>> GetReviews(int restaurantId)
    {
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
        if (restaurant is null)
        {
            return NotFound();
        }
        var reviews = _reviewRepository.Reviews?.Where(r => r.RestaurantId == restaurantId);
        var reviewDtos = _mapper.Map<IEnumerable<RestaurantReviewDto>>(reviews);
        return Ok(reviewDtos);
    }

    [HttpPost]
    public async Task<IActionResult> AddReview(int restaurantId, RestaurantReviewForUpdateDto reviewDto)
    {
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
        if (restaurant is null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var newReview = _mapper.Map<RestaurantReview>(reviewDto);
        newReview.Restaurant = restaurant;
        await _reviewRepository.AddReviewAsync(newReview);
        await _reviewRepository.SaveChangesAsync();

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

        var review = await _reviewRepository.GetReviewAsync(reviewId);
        if (review is null)
        {
            return NotFound();
        }

        if(review.RestaurantId != restaurantId)
        {
            return BadRequest();
        }
        _reviewRepository.DeleteReview(review);
        await _restaurantRepository.SaveChangesAsync();
        return Ok();
    }


}


