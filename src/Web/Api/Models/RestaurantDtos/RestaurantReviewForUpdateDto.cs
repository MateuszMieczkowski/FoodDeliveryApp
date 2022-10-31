using System.ComponentModel.DataAnnotations;

namespace Web.Api.Models.RestaurantDtos;

public class RestaurantReviewForUpdateDto
{

    [MaxLength(50)]
    [Required]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    [Required]
    public string Description { get; set; } = string.Empty;

    [Range(0, 5)]
    [Required]
    public int Rating { get; set; }
}
