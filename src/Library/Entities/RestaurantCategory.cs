using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities;

public class RestaurantCategory
{
    public string Name { get; set; } = string.Empty;

    public ICollection<Restaurant>? Restaurants { get; set; }
}
