using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities;

public class ProductCategory
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<Product>? Products { get; set; }
}
