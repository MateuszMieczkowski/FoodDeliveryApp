using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities;

public class ShoppingCartItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public Guid ShoppingCartId { get; set; }

    public Product Product { get; set; } = default!;

    public int Quantity { get; set; }
}