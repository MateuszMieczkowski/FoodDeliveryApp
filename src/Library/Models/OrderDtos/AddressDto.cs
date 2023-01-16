using System.ComponentModel.DataAnnotations;

namespace Library.Models.OrderDtos;

public class AddressDto
{
    [Required]
    [MaxLength(40)]
    public string Street { get; set; } = string.Empty;

    [Required]
    [Range(0, 1000)]
    public int? BuildingNumber { get; set; }

    public int? ApartmentNumber { get; set; }

    [Required]
    [MaxLength(40)]
    public string City { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string ZipCode { get; set; } = string.Empty;
}
