using Library.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web.Api.Models.ProductDtos;
using Web.Api.Models.OrderDtos;

namespace Web.Api.Models.RestaurantDtos;

public class RestaurantDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public RestaurantCategoryDto RestaurantCategory { get; set; } = default!;

    public ICollection<ProductDto>? Products { get; set; }

    public ICollection<OrderDto>? Orders { get; set; }

    public ICollection<RestaurantReviewDto>? Reviews { get; set; }

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

    public string ImageUrl { get; set; } = string.Empty;
}
