using AutoMapper;
using Library.Entities;
using Library.Models.OrderDtos;

namespace Library.Profiles;

public class OrderProfile : Profile
{
	public OrderProfile()
	{
		CreateMap<Order, OrderDto>().ReverseMap();
		CreateMap<OrderItem, OrderItemDto>().ReverseMap();
    }
}
