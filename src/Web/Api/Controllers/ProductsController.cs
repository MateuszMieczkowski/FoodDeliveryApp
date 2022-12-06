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
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductsController(IMapper mapper, IProductRepository productRepository, IRestaurantRepository restaurantRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _restaurantRepository = restaurantRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts(int restaurantId)
    {
        var restuarant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
        if (restuarant is null)
        {
            return NotFound();
        }

        var products = _productRepository.Products.Where(r => r.Restaurant == restuarant);
        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
        return Ok(productDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(int restaurantId, ProductForUpdateDto productDto)
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

        var newProduct = _mapper.Map<Product>(productDto);
        newProduct.Restaurant = restaurant;

        var productCategory = _productRepository.Products.FirstOrDefault(r => r.Name == newProduct.Name)?.Category;
        if (productCategory is null)
        {
            return BadRequest($"There's not such productCategory as {productDto.Category.Name}");
        }

        newProduct.Category = productCategory;
        newProduct.Restaurant = restaurant;

        await _productRepository.AddProductAsync(newProduct);
        await _productRepository.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct(int restaurantId, int productId)
    {
        var restaurant = await _restaurantRepository.GetRestaurantAsync(restaurantId);
        if (restaurant is null)
        {
            return NotFound();
        }


        var wasDeleted = await _productRepository.DeleteProductAsync(productId);
        if (!wasDeleted)
        {
            return NotFound();
        }

        await _productRepository.SaveChangesAsync();
        return Ok();
    }
}