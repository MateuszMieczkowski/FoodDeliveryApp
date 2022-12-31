using AutoMapper;
using Library.DataPersistence;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Api.Models.ProductDtos;
using Web.Api.Services.Interfaces;

namespace Web.Api.Controllers;

[Route("api/restaurants/{restaurantId}/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductDto>>> GetProducts(int restaurantId)
    {
        var productDtos = await _productService.GetProductsAsync(restaurantId);
        return Ok(productDtos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(int restaurantId, ProductForUpdateDto dto)
    {
        await _productService.CreateProductAsync(restaurantId, dto);
        return NoContent();
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> DeleteProduct(int restaurantId, int productId)
    {
        await _productService.DeleteProductAsync(restaurantId, productId);
        return Ok();
    }
}