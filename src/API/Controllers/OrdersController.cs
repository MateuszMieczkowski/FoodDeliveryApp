using Library.Models.OrderDtos;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Library.Enums.Enums;

namespace API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrdersController : ControllerBase
{
	private readonly IOrderService _orderService;
	public OrdersController(IOrderService orderService)
	{
		_orderService = orderService;
	}

	[HttpPost]
	[Authorize(Roles = "admin,manager,user")]
	public async Task<ActionResult> CreateOrder([FromQuery] Guid? userId, [FromQuery] int? restaurantId, [FromBody] AddressDto? addressDto)
	{
		await _orderService.CreateOrderAsync(userId, restaurantId, addressDto);

		return NoContent();
	}

	[HttpGet("{orderId:Guid}")]
    [Authorize(Roles = "admin,manager,user")]
    public async Task<ActionResult<OrderDto>> GetOrder([FromRoute] Guid orderId)
	{
		var order = await _orderService.GetOrderAsync(orderId);

		return Ok(order);
	}

    [HttpDelete("{orderId:Guid}")]
    [Authorize(Roles = "admin,manager")]
    public async Task<ActionResult<OrderDto>> DeleteOrder([FromRoute] Guid orderId)
    {
		await _orderService.DeleteOrder(orderId);

		return NoContent();
    }

	[HttpPatch("{orderId:Guid}")]
	[Authorize(Roles = "admin,manager")]
	public async Task<ActionResult> UpdateOrderStatus([FromRoute] Guid orderId, [FromQuery] OrderStatus? status)
	{

		await _orderService.UpdateOrderStatusAsync(orderId, status);
		return Ok();
	}

}
