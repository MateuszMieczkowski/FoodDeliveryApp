using AutoMapper;
using Library.Entities;
using Web.Api.Models.UserDtos;

namespace Web.Api.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserRegistrationDto, User>();
        }
    }
}
