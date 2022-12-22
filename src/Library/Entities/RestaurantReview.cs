using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Library.Entities;

public class RestaurantReview
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Range(0,5)]
    public int Rating { get; set; }

    public Restaurant Restaurant { get; set; } = default!;

    public int RestaurantId { get; set; }
}
