﻿using API.Models.RestaurantDtos;
using AutoMapper;
using Library.Entities;

namespace API.Profiles
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
