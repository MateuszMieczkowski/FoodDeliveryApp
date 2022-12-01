using AutoMapper;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Api.Models.ProductDtos;

namespace Web.Api.Controllers;

[Route("api/restaurants/{restaurantId}/products")]
[ApiController]
public class ProductsController : ControllerBase
{
	private readonly IRestaurantRepository _restaurantRepository;
	private readonly IMapper _mapper;
	public ProductsController(IRestaurantRepository restaurantRepository, IMapper mapper)
	{
		_restaurantRepository = restaurantRepository;
		_mapper = mapper;
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(int restaurantId)
	{
		var restuarant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
		if(restuarant is null)
		{
			return NotFound();
		}
		var productDtos = _mapper.Map<IEnumerable<ProductDto>>(restuarant.Products);
		return Ok(productDtos);
	}

}
