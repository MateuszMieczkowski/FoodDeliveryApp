using AutoMapper;
using Library.Entities;
using Web.Api.Models.RestaurantDtos;

namespace Web.Api.Profiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantDto>().ReverseMap().PreserveReferences(); 
            CreateMap<Restaurant, RestaurantForUpdateDto>().ReverseMap().PreserveReferences();
            CreateMap<RestaurantCategory,RestaurantCategoryDto>().ReverseMap().PreserveReferences();
            CreateMap<RestaurantReview, RestaurantReviewDto>().ReverseMap().PreserveReferences();
        }
    }
}
