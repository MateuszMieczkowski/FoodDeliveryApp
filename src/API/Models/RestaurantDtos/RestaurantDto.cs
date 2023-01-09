namespace API.Models.RestaurantDtos;

public class RestaurantDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string RestaurantCategoryName { get; set; } = default!;  

    public double Rating { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
}
