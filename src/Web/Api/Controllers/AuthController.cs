using Microsoft.AspNetCore.Mvc;
using Web.Api.Models.UserDtos;
using Web.Api.Services.Interfaces;

namespace Web.Api.Controllers
{
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
        public async Task<IActionResult> RegisterUser(UserRegistrationDto dto)
        {
            var result = await _userService.RegisterUserAsync(dto);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(UserLoginDto dto)
        {
            var token = await _userService.GetTokenAsync(dto);
            return Ok(token);
        }
    }
}
