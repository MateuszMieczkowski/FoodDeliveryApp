using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Entities;

public class RestaurantReview
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(50)]
    [Required]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500)]
    [Required]
    public string Description { get; set; } = string.Empty;

    [Range(0,5)]
    [Required]
    public int Rating { get; set; }

    [ForeignKey("RestaurantId")]
    public Restaurant Restaurant { get; set; } = default!;

    public int RestaurantId { get; set; }
}
