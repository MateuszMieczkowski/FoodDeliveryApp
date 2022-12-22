using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Library.Entities;

public class OrderItem
{
    public int Id { get; set; }

    public Order Order { get; set; } = default!;

    public Guid OrderId { get; set; }

    public int ProductQuantity { get; set; }

    public Product Product { get; set; } = default!;

    public int ProductId { get; set; }

}
