using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Library.Enums.Enums;
using Newtonsoft.Json;

namespace Library.Entities;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime Created { get; set; }

    public OrderStatus Status { get; set; }

    [ForeignKey("RestaurantId")]
    public Restaurant Restaurant { get; set; } = default!;

    [JsonIgnore]
    public int RestaurantId { get; set; } 

    [NotMapped]
    public decimal Total
    {
        get => OrderItems.Sum(o => o.ProductQuantity * o.Product.Price);
    }
}