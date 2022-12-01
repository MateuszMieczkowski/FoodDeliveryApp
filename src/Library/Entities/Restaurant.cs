﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Entities;

public class Restaurant
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(70)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(100)]
    public string City { get; set; } = string.Empty;

    public RestaurantCategory RestaurantCategory { get; set; } = default!;

    public ICollection<Product>? Products { get; set; }

    public ICollection<Order>? Orders { get; set; }

    public ICollection<RestaurantReview>? Reviews { get; set; }

    [NotMapped]
    public double Rating
    {
        get
        {
            if(Reviews is null || Reviews.Count == 0)
            {
                return 0.0;
            }
            return Reviews.Average(r => r.Rating);
        }
    }

    [MaxLength(200)]
    public string ImageUrl { get; set; } = string.Empty;

}
