﻿using static Library.Enums.Enums;

namespace Library.Models.OrderDtos;

public class OrderDto
{
    public Guid Id { get; set; }

    public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

    public DateTime Created { get; set; }

    public OrderStatus Status { get; set; }

    public int RestaurantId { get; set; }

    public AddressDto Address { get; set; } = default!;

    public Guid UserId { get; set; }

    public decimal Total { get; set; }
}
