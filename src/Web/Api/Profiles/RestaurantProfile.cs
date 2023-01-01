using AutoMapper;
using Library.Entities;
using Web.Api.Models.RestaurantDtos;

namespace Web.Api.Profiles
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantDto>().ReverseMap();
            CreateMap<Restaurant, RestaurantForUpdateDto>().ReverseMap();
            CreateMap<RestaurantCategory, RestaurantCategoryDto>().ReverseMap();
            CreateMap<RestaurantReview, RestaurantReviewDto>().ReverseMap();
            CreateMap<RestaurantReview, RestaurantReviewForUpdateDto>().ReverseMap();
        }
    }
}
