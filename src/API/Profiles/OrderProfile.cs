using API.Models.OrderDtos;
using AutoMapper;
using Library.Entities;

namespace API.Profiles;

public class OrderProfile : Profile
{
	public OrderProfile()
	{
		CreateMap<Order, OrderDto>().ReverseMap();
		CreateMap<OrderItem, OrderItemDto>().ReverseMap();
    }
}
