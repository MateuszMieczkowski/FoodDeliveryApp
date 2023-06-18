using Library.Models.ShoppingCartDtos;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/shoppingCart")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    [HttpGet]
    public async Task<ActionResult<ShoppingCartDto>> GetCart([FromQuery] int restaurantId, [FromQuery] Guid shoppingCartId)
    {
        var cart = await _shoppingCartService.GetShoppingCartAsync(restaurantId, shoppingCartId);
        return Ok(cart);
    }

    [HttpPost]
    public async Task<ActionResult> AddToCart([FromQuery] int productId, [FromQuery] Guid shoppingCartId)
    {
        await _shoppingCartService.AddToCartAsync(productId, shoppingCartId);
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteFromCart([FromQuery] int productId, [FromQuery] Guid shoppingCartId)
    {
        await _shoppingCartService.DeleteFromCartAsync(productId, shoppingCartId);
        return Ok();
    }

}