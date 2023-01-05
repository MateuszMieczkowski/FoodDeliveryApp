using API.Models.ShoppingCartDtos;
using API.Services.ShoppingCart;
using AutoMapper;
using Library.Entities;

namespace API.Profiles
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemDto>().ReverseMap();
        }
    }
}
