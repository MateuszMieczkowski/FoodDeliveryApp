using AutoMapper;
using Library.DataPersistence;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Api.Models.ProductDtos;

namespace Web.Api.Controllers;

[Route("api/restaurants/{restaurantId}/products")]
[ApiController]
public class ProductsController : ControllerBase
{
	private readonly IRestaurantRepository _restaurantRepository;
	private readonly IMapper _mapper;
	private readonly ApplicationDbContext _dbContext;

	public ProductsController(IRestaurantRepository restaurantRepository, IMapper mapper, ApplicationDbContext dbContext)
	{
		_restaurantRepository = restaurantRepository;
		_mapper = mapper;
		_dbContext = dbContext;
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

	[HttpPost]
	public async Task<IActionResult> CreateProduct(int restaurantId, ProductForUpdateDto productDto)
	{
		var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
		if(restaurant is null)
		{
			return NotFound();
		}
		if(!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}
		var newProduct = _mapper.Map<Product>(productDto);
		newProduct.Restaurant = restaurant;
		var productCategory = await _dbContext.ProductCategories.FirstOrDefaultAsync(r => r.Name == productDto.Category.Name);
		if(productCategory is null)
		{
			return BadRequest($"There's not such productCategory as { productDto.Category.Name }");
		}
		restaurant.Products!.Add(newProduct);
		await _restaurantRepository.SaveChangesAsync();
		return NoContent();
	}

	

}
