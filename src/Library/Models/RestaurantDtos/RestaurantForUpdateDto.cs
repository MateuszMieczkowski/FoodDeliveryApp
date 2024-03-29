﻿using System.ComponentModel.DataAnnotations;

namespace Library.Models.RestaurantDtos;

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
    public string RestaurantCategoryName { get; set; } = default!;

}
