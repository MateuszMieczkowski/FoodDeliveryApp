using Microsoft.AspNetCore.Identity;
using Web.Api.Models.UserDtos;

namespace Web.Api.Services.Interfaces;

public interface IUserService
{
    Task<IdentityResult> RegisterUserAsync(UserRegistrationDto dto);
    Task<string> GetTokenAsync(UserLoginDto dto);
}
