using API.Models.UserDtos;
using Microsoft.AspNetCore.Identity;

namespace API.Services.Interfaces;

public interface IUserService
{
    Task<IdentityResult> RegisterUserAsync(UserRegistrationDto dto);
    Task<string> GetTokenAsync(UserLoginDto dto);
}
