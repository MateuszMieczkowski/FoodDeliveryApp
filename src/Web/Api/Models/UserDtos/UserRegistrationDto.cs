using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Web.Api.Models.UserDtos;

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
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string Password { get; set; } = string.Empty;

    [JsonIgnore]
    public string UserName { get => Email; }
}

