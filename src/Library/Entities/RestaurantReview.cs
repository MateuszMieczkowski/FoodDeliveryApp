using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Entities;

public class RestaurantReview
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [MaxLength(50)]
    public string? Title { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [Range(0,5)]
    public int Rating { get; set; }

    [ForeignKey("RestaurantId")]
    public Restaurant Restaurant { get; set; } = default!;

    public int RestaurantId { get; set; }
}
