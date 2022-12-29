using Microsoft.AspNetCore.Mvc;
using Web.Api.Models;
using Web.Api.Models.RestaurantDtos;
using Web.Api.Services.Interfaces;

namespace Web.Api.Controllers;

[ApiController]
[Route("/api/restaurants")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantService _restaurantService;

    public RestaurantsController(IRestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<RestaurantDto>>> GetRestaurants
        (string? name, string? city, string? category, string? searchQuery, int pageNumber = 1, int pageSize = 10)
    {
        var result = await _restaurantService.GetRestaurantsAsync(name, city, category, searchQuery, pageNumber, pageSize);
        return Ok(result);
    }

    [HttpGet("{restaurantId}", Name = "GetRestaurant")]
    public async Task<ActionResult<RestaurantDto>> GetRestaurant(int restaurantId)
    {
        var restaurantDto = await _restaurantService.GetRestaurantAsync(restaurantId);
        return Ok(restaurantDto);
    }

    [HttpDelete("{restaurantId}")]
    public async Task<IActionResult> DeleteRestaurant(int restaurantId)
    {
        await _restaurantService.DeleteRestaurantAsync(restaurantId);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(RestaurantForUpdateDto dto)
    {
        var newRestaurant = await _restaurantService.CreateRestaurantAsync(dto);
        return CreatedAtRoute("GetRestaurant", new { restaurantId = newRestaurant.Id }, newRestaurant);
    }

    [HttpPut("{restaurantId}")]
    public async Task<IActionResult> UpdateRestaurant(int restaurantId, RestaurantForUpdateDto dto)
    {
        await _restaurantService.UpdateRestaurantAsync(restaurantId, dto);
        return NoContent();
    }

    [HttpGet("categories")]
    public ActionResult<IEnumerable<RestaurantCategoryDto>> GetCategories()
    {
        var categoryDtos = _restaurantService.GetRestaurantCategories();
        return Ok(categoryDtos);
    }
}
