using System.ComponentModel.DataAnnotations;

namespace Library.Entities;

public class Address
{
    public string Street { get; set; } = string.Empty;

    public int BuildingNumber { get; set; } 

    public int? ApartmentNumber { get; set; }

    public string City { get; set; } = string.Empty;

    public string ZipCode { get; set; } = string.Empty;
}