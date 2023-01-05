﻿using API.Models.UserDtos;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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
            dto.RoleName = "user";
            var result = await _userService.RegisterUserAsync(dto);

            return Ok(result);
        }

        [HttpPost("register-manager")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> RegisterManager(UserRegistrationDto dto)
        {
            dto.RoleName = "manager";
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
