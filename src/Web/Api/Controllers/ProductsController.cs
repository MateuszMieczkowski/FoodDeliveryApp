using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;

[Route("api/restaurants/{restaurantId}/products")]
[ApiController]
public class ProductsController : ControllerBase
{
	private readonly IRestaurantRepository _restaurantRepository;
	public ProductsController(IRestaurantRepository restaurantRepository)
	{
		_restaurantRepository = restaurantRepository;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<Product>>> GetProducts(int restaurantId)
	{
		var restuarant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
		if(restuarant is null)
		{
			return NotFound();
		}

		return Ok(restuarant.Products);
	}
}
