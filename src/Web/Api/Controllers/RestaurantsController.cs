using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Models;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("/api/restaurants")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;
        public RestaurantsController(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetRestaurants()
        {
            IEnumerable<RestaurantDto> restaurantDtos = _restaurantRepository.AllRestaurants.Select(r => r.ToDto());
            return Ok(restaurantDtos);
        }

        [HttpGet("{id}")]
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
        public async Task<ActionResult<RestaurantDto>> DeleteRestaurant(int id)
        {
            var restaurant = await _restaurantRepository.GetRestaurantAsync(id);
            if(restaurant is null)
            {
                return NotFound();
            }
            await _restaurantRepository.DeleteRestaurantAsync(restaurant);

            return NoContent();
        }

    }
}
