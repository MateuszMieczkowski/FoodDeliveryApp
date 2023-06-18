using Library.Models.ProductDtos;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/restaurants/{restaurantId:int}/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetProducts([FromRoute] int restaurantId)
    {
        var productDtos = await _productService.GetProductsAsync(restaurantId);
        return Ok(productDtos);
    }

    [HttpPost]
    [Authorize(Roles="manager, admin")]
    public async Task<ActionResult> CreateProduct([FromRoute] int restaurantId, [FromBody] ProductForUpdateDto dto)
    {
        await _productService.CreateProductAsync(restaurantId, dto);
        return NoContent();
    }

    [HttpDelete("{productId:int}")]
    [Authorize(Roles="manager, admin")]
    public async Task<ActionResult> DeleteProduct([FromRoute] int productId)
    {
        await _productService.DeleteProductAsync(productId);
        return Ok();
    }
}