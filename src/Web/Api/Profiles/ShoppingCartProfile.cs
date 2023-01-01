using AutoMapper;
using Library.Entities;
using Library.Services.ShoppingCart;
using Web.Api.Models.ShoppingCartDtos;

namespace Web.Api.Profiles
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
