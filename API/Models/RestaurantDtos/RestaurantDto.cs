using Library.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web.Api.Models.ProductDtos;
using Web.Api.Models.OrderDtos;

namespace Web.Api.Models.RestaurantDtos;

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
