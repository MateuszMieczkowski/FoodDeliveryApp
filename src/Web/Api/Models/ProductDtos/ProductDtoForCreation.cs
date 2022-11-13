using Library.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Api.Models.ProductDtos;

public class ProductDtoForCreation
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
    public ProductCategory Category { get; set; } = default!;

    [Required]
    public Restaurant Restaurant { get; set; } = default!;

    [Required]
    public string ImageUrl { get; set; } = string.Empty;
}
