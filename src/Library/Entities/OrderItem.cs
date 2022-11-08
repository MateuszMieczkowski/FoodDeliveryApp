using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Library.Entities;

public class OrderItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("OrderId")]
    public Order Order { get; set; } = default!;

    [JsonIgnore]
    public Guid OrderId { get; set; }

    public int ProductQuantity { get; set; }

    [ForeignKey("ProductId")]
    public Product Product { get; set; } = default!;

    [JsonIgnore]
    public int ProductId { get; set; }
}
