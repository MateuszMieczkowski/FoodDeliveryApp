using Library.Entities;
using System.ComponentModel.DataAnnotations;

namespace Web.Api.Models.RestaurantDtos;

public class RestaurantCategoryDto
{
    public string Name { get; set; } = string.Empty;
}
