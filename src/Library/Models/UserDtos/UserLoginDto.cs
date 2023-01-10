using System.ComponentModel.DataAnnotations;

namespace Library.Models.UserDtos;

public class UserLoginDto
{
    [Required]
    [EmailAddress]
    [MaxLength(80)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Password { get; set; } = string.Empty;
 }
