using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Entities;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime Created { get; set; }

    [ForeignKey("RestaurantId")]
    public Restaurant Restaurant { get; set; } = default!;

    public int RestaurantId { get; set; } 

    [NotMapped]
    public decimal Total
    {
        get => OrderItems.Sum(o => o.ProductQuantity * o.Product.Price);
    }
}