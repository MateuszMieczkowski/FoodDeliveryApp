using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace API.Models.UserDtos;

public class UserRegistrationDto
{
    [Required]
    [MaxLength(30)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(30)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [MaxLength(80)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;
    
    [MaxLength(20)]
    public string RoleName { get; set; } = "user";
    
    public int? ManagedRestaurantId { get; set; }

    [JsonIgnore]
    public string UserName => Email;
}

