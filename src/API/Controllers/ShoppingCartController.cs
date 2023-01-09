using API.Models.ShoppingCartDtos;
using API.Services.Interfaces;
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
    public ActionResult<ShoppingCartDto> GetCart()
    {
        var cart = _shoppingCartService.GetShoppingCart();
        return Ok(cart);
    }

    [HttpPost]
    public async Task<ActionResult> AddToCart(int? productId)
    {
        if (!productId.HasValue)
        {
            return BadRequest();
        }
        await _shoppingCartService.AddToCartAsync(productId.Value);
        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteFromCart(int? productId)
    {
        if (!productId.HasValue)
        {
            return BadRequest();
        }
        await _shoppingCartService.DeleteFromCartAsync(productId.Value);
        return Ok();
    }

}