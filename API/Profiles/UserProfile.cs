using API.Models.UserDtos;
using AutoMapper;
using Library.Entities;

namespace API.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRegistrationDto, User>();
    }
}