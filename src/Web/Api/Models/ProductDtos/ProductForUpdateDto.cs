using Library.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web.Api.Models.RestaurantDtos;

namespace Web.Api.Models.ProductDtos;

public class ProductForUpdateDto
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public decimal Price { get; set; }

    [Required]
    public bool InStock { get; set; }

    [Required]
    public ProductCategoryDto Category { get; set; } = default!;

    [Required]
    public string ImageUrl { get; set; } = string.Empty;
}
