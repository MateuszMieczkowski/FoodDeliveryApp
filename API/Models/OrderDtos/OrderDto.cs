using static Library.Enums.Enums;

namespace API.Models.OrderDtos;

public class OrderDto
{
    public Guid Id { get; set; }

    public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();

    public DateTime Created { get; set; }

    public OrderStatus Status { get; set; }

   public int RestaurantId { get; set; }

    public decimal Total
    {
        get => OrderItems.Sum(o => o.ProductQuantity * o.Product.Price);
    }
}
