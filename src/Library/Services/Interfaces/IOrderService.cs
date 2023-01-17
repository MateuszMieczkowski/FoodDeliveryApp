using Library.Models;
using Library.Models.OrderDtos;
using static Library.Enums.Enums;

namespace Library.Services.Interfaces;

public interface IOrderService
{
    Task CreateOrderAsync(Guid? userId, int? restaurantId, AddressDto? addressDto);

    Task DeleteOrder(Guid orderId);
    
    Task<OrderDto> GetOrderAsync(Guid orderId);

    Task UpdateOrderStatusAsync(Guid orderId, OrderStatus? status);

    Task<PagedResult<OrderDto>> GetOrdersAsync(Guid userId, int pageNumber, int pageSize);

    Task<PagedResult<OrderDto>> GetOrdersAsync(int restaurantId, int pageNumber, int pageSize);
}
