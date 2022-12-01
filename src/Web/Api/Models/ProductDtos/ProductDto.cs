﻿using Library.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web.Api.Models.RestaurantDtos;
using Web.Api.Models.OrderDtos;

namespace Web.Api.Models.ProductDtos;

public class ProductDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public bool InStock { get; set; }

    public ProductCategoryDto Category { get; set; } = default!;

    public RestaurantDto Restaurant { get; set; } = default!;

    public ICollection<OrderItemDto>? OrderItems { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
}