using Library.Entities;
using System.Runtime.CompilerServices;
using Web.Api.Models.RestaurantDtos;

namespace Web.Api;

public static class MappingExtensions
{
    public static Restaurant ToEntity(this RestaurantDto restaurantDto)
    {
        var restaurant = new Restaurant();
        restaurant.Id = restaurantDto.Id;
        restaurant.Name = restaurantDto.Name;
        restaurant.Description = restaurantDto.Description;
        restaurant.RestaurantCategory = restaurantDto.RestaurantCategory;
        restaurant.RestaurantCategoryName = restaurant.RestaurantCategory.Name;
        restaurant.Products = restaurantDto.Products;
        restaurant.Orders = restaurantDto.Orders;
        restaurant.Reviews = restaurantDto.Reviews;
        restaurant.City = restaurantDto.City;
        restaurant.ImageUrl = restaurantDto.ImageUrl;
        return restaurant;
    }

    public static RestaurantDto ToDto(this Restaurant restaurant)
    {
        var restaurantDto = new RestaurantDto();
        restaurantDto.Id = restaurant.Id;
        restaurantDto.Name = restaurant.Name;
        restaurantDto.Description = restaurant.Description;
        restaurantDto.RestaurantCategory = restaurant.RestaurantCategory;
        restaurantDto.Products = restaurant.Products;
        restaurantDto.Orders = restaurant.Orders;
        restaurantDto.Reviews = restaurant.Reviews;
        restaurantDto.City = restaurant.City;
        restaurantDto.ImageUrl = restaurant.ImageUrl;
        return restaurantDto;
    }

}
