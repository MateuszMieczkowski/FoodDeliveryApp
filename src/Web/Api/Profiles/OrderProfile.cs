using AutoMapper;
using Library.Entities;
using Web.Api.Models.OrderDtos;

namespace Web.Api.Profiles;

public class OrderProfile : Profile
{
	public OrderProfile()
	{
		CreateMap<Order, OrderDto>().ReverseMap().PreserveReferences();
		CreateMap<OrderItem, OrderItemDto>().ReverseMap().PreserveReferences();
    }
}
