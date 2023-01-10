using AutoMapper;
using Library.Entities;
using Library.Models.ShoppingCartDtos;
using Library.Services.ShoppingCart;

namespace Library.Profiles;

public class ShoppingCartProfile : Profile
{
    public ShoppingCartProfile()
    {
        CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();
        CreateMap<ShoppingCartItem, ShoppingCartItemDto>().ReverseMap();
    }
}