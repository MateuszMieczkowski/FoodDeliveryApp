﻿using AutoMapper;
using Library.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.Api.Exceptions;
using Web.Api.Models;
using Web.Api.Models.UserDtos;
using Web.Api.Services.Interfaces;

namespace Web.Api.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly JwtSettings _jwtSettings;

    public UserService(UserManager<User> userManager, IMapper mapper, JwtSettings jwtSettings)
    {
        _userManager = userManager;
        _mapper = mapper;
        _jwtSettings = jwtSettings;
    }

    private JwtSecurityToken CreateToken(IList<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.JwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expireDate = DateTime.Now.AddDays(_jwtSettings.JwtExpireDays);

        var token = new JwtSecurityToken(_jwtSettings.JwtIssuer,
                                         _jwtSettings.JwtIssuer,
                                         claims: claims,
                                         expires: expireDate,
                                         signingCredentials: credentials);
        return token;
    }

    private async Task<List<Claim>> CreateClaimsAsync(User user)
    {
        var claims = await _userManager.GetClaimsAsync(user);
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.Add(new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"));

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims.ToList();
    }

    public async Task<IdentityResult> RegisterUserAsync(UserRegistrationDto dto)
    {
        var newUser = _mapper.Map<User>(dto);
        var createResult = await _userManager.CreateAsync(newUser, dto.Password);
        if (!createResult.Succeeded)
        {
            return createResult;
        }

        var addToRoleResult = await _userManager.AddToRoleAsync(newUser, dto.RoleName);
        if (!addToRoleResult.Succeeded)
        {
            return addToRoleResult;
        }

        var claims = await CreateClaimsAsync(newUser);
        var addClaimsResult = await _userManager.AddClaimsAsync(newUser, claims);
        if (!addClaimsResult.Succeeded)
        {
            return addClaimsResult;
        }

        return createResult;
    }

    public async Task<string> GetTokenAsync(UserLoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null)
        {
            throw new BadRequestException("Wrong Email or Password");
        }

        var checkPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!checkPassword)
        {
            throw new BadRequestException("Wrong Email or Password");
        }

        var claims = await _userManager.GetClaimsAsync(user);
        var token = CreateToken(claims);
        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }
}
