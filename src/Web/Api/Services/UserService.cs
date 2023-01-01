using AutoMapper;
using Library.Entities;
using Microsoft.AspNetCore.Identity;
using Web.Api.Models.UserDtos;
using Web.Api.Services.Interfaces;

namespace Web.Api.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public UserService(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<IdentityResult> RegisterUserAsync(UserRegistrationDto dto)
    {
        var newUser = _mapper.Map<User>(dto);
        var result = await _userManager.CreateAsync(newUser, dto.Password);
        return result;
    }
}
