﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Library.Enums.Enums;

namespace Library.Entities;

public sealed class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
   
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public bool InStock { get; set; }

    public ProductSize ProductSize { get; set; }

    [ForeignKey("CategoryId")]
    public ProductCategory Category { get; set; } = default!;

    public int CategoryId { get; set; }

    [ForeignKey("RestaurantId")]
    public Restaurant Restaurant { get; set; } = default!;

    public int RestaurantId { get; set; }

    public ICollection<OrderItem>? OrderItems { get; set; }
}