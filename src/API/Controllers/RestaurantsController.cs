using Library.Models;
using Library.Models.RestaurantDtos;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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

    [HttpGet("{restaurantId:int}", Name = "GetRestaurant")]
    public async Task<ActionResult<RestaurantDto>> GetRestaurant([FromRoute] int restaurantId)
    {
        var restaurantDto = await _restaurantService.GetRestaurantAsync(restaurantId);
        return Ok(restaurantDto);
    }

    [HttpDelete("{restaurantId:int}")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> DeleteRestaurant([FromRoute] int restaurantId)
    {
        await _restaurantService.DeleteRestaurantAsync(restaurantId);
        return NoContent();
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult> CreateRestaurant([FromBody] RestaurantForUpdateDto dto)
    {
        var newRestaurant = await _restaurantService.CreateRestaurantAsync(dto);
        return CreatedAtRoute("GetRestaurant", new { restaurantId = newRestaurant.Id }, newRestaurant);
    }

    [HttpPut("{restaurantId:int}")]
    [Authorize(Roles = "admin,manager")]
    public async Task<ActionResult> UpdateRestaurant([FromRoute] int restaurantId, [FromBody] RestaurantForUpdateDto dto)
    {
        await _restaurantService.UpdateRestaurantAsync(restaurantId, dto);
        return NoContent();
    }

    [HttpGet("categories")]
    public ActionResult<List<RestaurantCategoryDto>> GetCategories()
    {
        var categoryDtos = _restaurantService.GetRestaurantCategories().ToList();
        return Ok(categoryDtos);
    }

	[HttpGet("cities")]
	public async Task<ActionResult<List<string>>> GetCities()
	{
        var cities = await _restaurantService.GetCities();
		return Ok(cities);
	}
}
