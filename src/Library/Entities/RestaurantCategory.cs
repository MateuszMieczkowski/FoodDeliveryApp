using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities;

public sealed class RestaurantCategory
{
   
    [Key]
    [MaxLength(20)]
    public string Name { get; set; } = string.Empty;

    public ICollection<Restaurant>? Restaurants { get; set; }
}
