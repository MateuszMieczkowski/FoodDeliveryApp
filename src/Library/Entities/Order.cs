using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Library.Enums.Enums;
using Newtonsoft.Json;

namespace Library.Entities;

public class Order
{
    public Guid Id { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public DateTime Created { get; set; }

    public OrderStatus Status { get; set; }

    public Restaurant Restaurant { get; set; } = default!;

    public int RestaurantId { get; set; }

    public decimal Total { get; set; }

    public User User { get; set; } = default!;

    public Guid UserId { get; set; }
}