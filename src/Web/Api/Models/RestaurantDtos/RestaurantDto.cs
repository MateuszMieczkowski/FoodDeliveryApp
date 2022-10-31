using Library.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Api.Models.RestaurantDtos;

public class RestaurantDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    
    public string City { get; set; } = string.Empty;

    public string ImageUrl { get; set; } = string.Empty;
    
    public RestaurantCategory RestaurantCategory { get; set; } = default!;

    public ICollection<Product>? Products { get; set; }

    public ICollection<Order>? Orders { get; set; }

    public ICollection<RestaurantReview>? Reviews { get; set; }

    public double Rating
    {
        get
        {
            if (Reviews is null || Reviews.Count == 0)
            {
                return 0.0;
            }
            return Reviews.Average(r => r.Rating);
        }
    }

}
