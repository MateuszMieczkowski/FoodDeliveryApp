﻿using System.ComponentModel.DataAnnotations;

namespace Library.Models.ProductDtos;

public class ProductForUpdateDto
{
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public decimal? Price { get; set; }

    [Required]
    public bool? InStock { get; set; }

    [Required]
    public int? ProductCategoryId { get; set; }

    [Required]
    public int? RestaurantId { get; set; }

    [Required]
    public string ImageUrl { get; set; } = string.Empty;
}
