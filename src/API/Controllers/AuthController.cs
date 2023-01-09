using API.Models.UserDtos;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(UserRegistrationDto dto)
    {
        dto.RoleName = "user";
        var result = await _userService.RegisterUserAsync(dto);

        return Ok(result);
    }

    [HttpPost("register-manager")]
    [Authorize(Roles ="admin")]
    public async Task<ActionResult> RegisterManager(UserRegistrationDto dto)
    {
        dto.RoleName = "manager";
        var result = await _userService.RegisterUserAsync(dto);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> LoginUser(UserLoginDto dto)
    {
        var token = await _userService.GetTokenAsync(dto);
        return Ok(token);
    }
}