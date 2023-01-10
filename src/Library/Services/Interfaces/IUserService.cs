using Library.Models.UserDtos;
using Microsoft.AspNetCore.Identity;

namespace Library.Services.Interfaces;

public interface IUserService
{
    Task<IdentityResult> RegisterUserAsync(UserRegistrationDto dto);
    Task<string> GetTokenAsync(UserLoginDto dto);
}