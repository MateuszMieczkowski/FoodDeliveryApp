namespace API.Models;

public class JwtSettings
{
    public string JwtKey { get; set; } = string.Empty;

    public string JwtIssuer { get; set; } = string.Empty;

    public int JwtExpireDays{ get; set; }
}
