using AutoMapper;
using Library.Entities;
using Library.Models.UserDtos;

namespace Library.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserRegistrationDto, User>();
    }
}