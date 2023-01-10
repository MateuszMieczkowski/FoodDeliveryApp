using Library.Models.ProductDtos;

namespace Library.Models.OrderDtos;

public class OrderItemDto
{
    public int Id { get; set; }

    public Guid OrderId { get; set; }

    public int ProductQuantity { get; set; }

    public ProductDto Product { get; set; } = default!;
}
