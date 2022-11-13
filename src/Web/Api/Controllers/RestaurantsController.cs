using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Web.Api.Models.RestaurantDtos;

namespace Web.Api.Controllers;

[ApiController]
[Route("/api/restaurants")]
public class RestaurantsController : ControllerBase
{
    private readonly IRestaurantRepository _restaurantRepository;
    private readonly IRestaurantCategoryRepository _restaurantCategoryRepository;
   
    public RestaurantsController(IRestaurantRepository restaurantRepository, IRestaurantCategoryRepository restaurantCategoryRepository)
    {
        _restaurantRepository = restaurantRepository;
        _restaurantCategoryRepository = restaurantCategoryRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants(string? name, string? city, string? category, string? searchQuery, int pageNumber, int pageSize)
    {
        var restaurants = await _restaurantRepository.GetRestaurantsAsync(name, city, category, searchQuery, pageNumber, pageSize);
        return Ok(restaurants);
    }

    [HttpGet("{restaurantId}", Name = "GetRestaurant")]
    public async Task<ActionResult<Restaurant>> GetRestaurant(int restaurantId)
    {
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId);

        if (restaurant is null)
        {
            return NotFound();
        }
        return Ok(restaurant);
    }

    [HttpDelete("{restaurantId}")]
    public async Task<IActionResult> DeleteRestaurant(int restaurantId)
    {
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
        if (restaurant is null)
        {
            return NotFound();
        }
        await _restaurantRepository.DeleteRestaurantAsync(restaurant);

        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(RestaurantForUpdateDto restaurantForCreationDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = await _restaurantCategoryRepository.GetRestaurantCategory(restaurantForCreationDto.RestaurantCategory.Name);
        if (category is null)
        {
            category = new RestaurantCategory() { Name = restaurantForCreationDto.RestaurantCategory.Name };
        }

        var newRestaurant = new Restaurant()
        {
            Name = restaurantForCreationDto.Name,
            Description = restaurantForCreationDto.Description,
            RestaurantCategory = category,
            RestaurantCategoryName = category.Name,
            City = restaurantForCreationDto.City,
            ImageUrl = restaurantForCreationDto.ImageUrl
        };
        await _restaurantRepository.AddRestaurantAsync(newRestaurant);

        return CreatedAtRoute("GetRestaurant", new { restaurantId = newRestaurant.Id }, newRestaurant);
    }
    [HttpPut("{restaurantId}")]
    public async Task<IActionResult> UpdateRestaurant(int restaurantId, RestaurantForUpdateDto restaurantDto)
    {
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
        if (restaurant is null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = await _restaurantCategoryRepository.GetRestaurantCategory(restaurantDto.RestaurantCategory.Name);

        if (category is null)
        {
            category = new RestaurantCategory() { Name = restaurantDto.Name };
            await _restaurantCategoryRepository.AddCategoryAsync(category);
        }

        restaurant.Name = restaurantDto.Name;
        restaurant.Description = restaurantDto.Description;
        restaurant.RestaurantCategory = category;
        restaurant.RestaurantCategoryName = category.Name;

        await _restaurantRepository.SaveChangesAsync();
        await _restaurantCategoryRepository.SaveChangesAsync();

        return NoContent();
    }

    [HttpPatch("{restaurantId}")]
    public async Task<IActionResult> UpdateRestaurant(int restaurantId, JsonPatchDocument<RestaurantForUpdateDto> jsonPatchDocument)
    {
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId);

        if (restaurant is null)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var updatedRestaurantDto = new RestaurantForUpdateDto()
        {
            Name = restaurant.Name,
            Description = restaurant.Description,
            RestaurantCategory = restaurant.RestaurantCategory
        };


        try
        {
            jsonPatchDocument.ApplyTo(updatedRestaurantDto);
        }
        catch (Exception)
        {
            return BadRequest();
        }


        if (!TryValidateModel(updatedRestaurantDto))
        {
            return BadRequest(ModelState);
        }

        var category = await _restaurantCategoryRepository.GetRestaurantCategory(updatedRestaurantDto.RestaurantCategory.Name);
        if (category is null)
        {
            category = new RestaurantCategory() { Name = updatedRestaurantDto.Name };
            await _restaurantCategoryRepository.AddCategoryAsync(category);
        }

        restaurant.Name = updatedRestaurantDto.Name;
        restaurant.Description = updatedRestaurantDto.Description;
        restaurant.RestaurantCategory = category;
        restaurant.RestaurantCategoryName = category.Name;

        await _restaurantRepository.SaveChangesAsync();
        await _restaurantCategoryRepository.SaveChangesAsync();

        return NoContent();
    }
}
