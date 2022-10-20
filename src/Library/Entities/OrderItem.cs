using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Entities;

public class OrderItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("OrderId")]
    public Order Order { get; set; } = default!;
    public int OrderId { get; set; }

    public int ProductQuantity { get; set; }

    [ForeignKey("ProductId")]
    public Product Product { get; set; } = default!;

    public int ProductId { get; set; }
}
