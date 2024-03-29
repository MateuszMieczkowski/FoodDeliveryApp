﻿namespace Library.Models.RestaurantDtos;

public class RestaurantReviewDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Rating { get; set; }
}
