using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Models.RestaurantDtos;

namespace Web.Api.Controllers
{
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
        public ActionResult<IEnumerable<RestaurantDto>> GetRestaurants()
        {
            IEnumerable<RestaurantDto> restaurantDtos = _restaurantRepository.AllRestaurants.Select(r => r.ToDto());
            return Ok(restaurantDtos);
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
        public async Task<ActionResult<RestaurantDto>> CreateRestaurant(RestaurantForCreationDto restaurantForCreationDto)
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

    }
}
