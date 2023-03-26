using static Library.Enums.Enums;

namespace Library.Entities;

public class Order
{
    public Guid Id { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public DateTime Created { get; set; }

    public OrderStatus Status { get; set; }
    
    public decimal Total { get; set; }
    
    
    public decimal DiscountAmount { get; set; }
    
    
    public decimal DiscountedTotal { get; set; }
    
    public Discount? Discount { get; set; }
    
    public int? DiscountId { get; set; }
    
    public Restaurant Restaurant { get; set; } = default!;

    public int RestaurantId { get; set; }

    public User User { get; set; } = default!;

    public Guid UserId { get; set; }

    public Address? Address { get; set; }
    
}