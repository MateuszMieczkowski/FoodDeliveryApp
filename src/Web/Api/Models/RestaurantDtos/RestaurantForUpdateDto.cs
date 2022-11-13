using Library.Entities;
using System.ComponentModel.DataAnnotations;

namespace Web.Api.Models.RestaurantDtos;

public class RestaurantForUpdateDto
{
    [Required]
    [MaxLength(70)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string City { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string ImageUrl { get; set; } = string.Empty;

    [Required]
    public RestaurantCategory RestaurantCategory { get; set; } = default!;

    public ICollection<Product>? Products { get; set; }

    public ICollection<Order>? Orders { get; set; }

    public ICollection<RestaurantReview>? Reviews { get; set; }
}
