using AutoMapper;
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
    private readonly IMapper _mapper;

    public RestaurantsController(IRestaurantRepository restaurantRepository, IRestaurantCategoryRepository restaurantCategoryRepository, IMapper mapper)
    {
        _restaurantRepository = restaurantRepository;
        _restaurantCategoryRepository = restaurantCategoryRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetRestaurants
        (string? name, string? city, string? category,  string? searchQuery, int pageNumber, int pageSize)
    {
        var restaurants = await _restaurantRepository.GetRestaurantsAsync(name, city, category, searchQuery, pageNumber, pageSize);
        var restaurantDtos = _mapper.Map<IEnumerable<Restaurant>, IEnumerable<RestaurantDto>>(restaurants);
        return Ok(restaurantDtos);
    }

    [HttpGet("{restaurantId}", Name = "GetRestaurant")]
    public async Task<ActionResult<RestaurantDto>> GetRestaurant(int restaurantId)
    {
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId);

        if (restaurant is null)
        {
            return NotFound();
        }
        var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);
        return Ok(restaurantDto);
    }

    [HttpDelete("{restaurantId}")]
    public async Task<IActionResult> DeleteRestaurant(int restaurantId)
    {
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
        if (restaurant is null)
        {
            return NotFound();
        }
        _restaurantRepository.DeleteRestaurant(restaurant);
        await _restaurantRepository.SaveChangesAsync();
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

        var newRestaurant = _mapper.Map<Restaurant>(restaurantForCreationDto);
        newRestaurant.RestaurantCategory = category;

        await _restaurantRepository.AddRestaurantAsync(newRestaurant);
        await _restaurantRepository.SaveChangesAsync();
        var newRestaurantDto = _mapper.Map<RestaurantDto>(newRestaurant);
        return CreatedAtRoute("GetRestaurant", new { restaurantId = newRestaurant.Id }, newRestaurantDto);
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
            category = new RestaurantCategory() { Name = restaurantDto.RestaurantCategory.Name };
        }

        restaurant.Name = restaurantDto.Name;
        restaurant.Description = restaurantDto.Description;
        restaurant.RestaurantCategory = category;

        await _restaurantRepository.SaveChangesAsync();

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

        var updatedRestaurantDto = _mapper.Map<RestaurantForUpdateDto>(restaurant);
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
            category = new RestaurantCategory() { Name = updatedRestaurantDto.RestaurantCategory.Name };
        }

        restaurant.Name = updatedRestaurantDto.Name;
        restaurant.Description = updatedRestaurantDto.Description;
        restaurant.RestaurantCategory = category;
        restaurant.ImageUrl = updatedRestaurantDto.ImageUrl;
        await _restaurantRepository.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("categories")]
    public ActionResult<IEnumerable<RestaurantCategoryDto>> GetCategories()
    {
        var categoryDtos = _mapper.Map<IEnumerable<RestaurantCategoryDto>>(_restaurantCategoryRepository.Categories);
        return Ok(categoryDtos);
    }
}
