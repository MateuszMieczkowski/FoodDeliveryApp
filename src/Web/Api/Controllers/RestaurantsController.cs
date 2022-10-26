using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Web.Api.Models.RestaurantDtos;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("/api/restaurant")]
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
        public ActionResult<IEnumerable<RestaurantDto>> GetRestaurants()
        {
            IEnumerable<RestaurantDto> restaurants = _restaurantRepository.AllRestaurants.Select(r => r.ToDto());
            return Ok(restaurants);
        }

        [HttpGet("category/{category}")]
        public ActionResult<IEnumerable<RestaurantDto>> GetRestaurants(string category)
        {
            var restaurants = _restaurantRepository.AllRestaurants.Where(r => r.RestaurantCategoryName.ToLower() == category).Select(r => r.ToDto());
            return Ok(restaurants);
        }

        [HttpGet("{id}", Name = "GetRestaurant")]
        public async Task<ActionResult<RestaurantDto>> GetRestaurant(int id)
        {
            var restaurant = await _restaurantRepository.GetRestaurantAsync(id);

            if (restaurant is null)
            {
                return NotFound();
            }

            var restaurantDto = restaurant.ToDto();
            return Ok(restaurantDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _restaurantRepository.GetRestaurantAsync(id);
            if (restaurant is null)
            {
                return NotFound();
            }
            await _restaurantRepository.DeleteRestaurantAsync(restaurant);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateRestaurant(RestaurantForCreationAndUpdateDto restaurantForCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _restaurantCategoryRepository.GetRestaurantCategory(restaurantForCreationDto.RestaurantCategory.Name);
            if(category is null)
            {
                category = new RestaurantCategory() { Name = restaurantForCreationDto.RestaurantCategory.Name };
            }

            var newRestaurant = new Restaurant()
            {
                Name = restaurantForCreationDto.Name,
                Description = restaurantForCreationDto.Description,
                RestaurantCategory = category,
                RestaurantCategoryName = category.Name,
            };
            await _restaurantRepository.AddRestaurantAsync(newRestaurant);

            return CreatedAtRoute("GetRestaurant", new { id = newRestaurant.Id }, newRestaurant.ToDto());
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRestaurant(int id, RestaurantForCreationAndUpdateDto restaurantDto)
        {
            var restaurant = await _restaurantRepository.GetRestaurantAsync(id);
            if(restaurant is null)
            {
                return NotFound();
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _restaurantCategoryRepository.GetRestaurantCategory(restaurantDto.RestaurantCategory.Name);

            if(category is null)
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

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateRestaurant(int id, JsonPatchDocument<RestaurantForCreationAndUpdateDto> jsonPatchDocument)
        {
            var restaurant = await _restaurantRepository.GetRestaurantAsync(id);

            if (restaurant is null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedRestaurantDto = new RestaurantForCreationAndUpdateDto()
            {
                Name = restaurant.Name,
                Description = restaurant.Description,
                RestaurantCategory = restaurant.RestaurantCategory
            };

            jsonPatchDocument.ApplyTo(updatedRestaurantDto);

            if(!TryValidateModel(updatedRestaurantDto))
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
}
