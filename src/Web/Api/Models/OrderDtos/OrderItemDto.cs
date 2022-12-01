using Library.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web.Api.Models.ProductDtos;

namespace Web.Api.Models.OrderDtos;

public class OrderItemDto
{
    public int Id { get; set; }

    public OrderDto Order { get; set; } = default!;

    public Guid OrderId { get; set; }

    public int ProductQuantity { get; set; }

    public ProductDto Product { get; set; } = default!;
}
