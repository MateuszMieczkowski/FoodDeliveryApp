using Library.Entities;
using System.Runtime.CompilerServices;
using Web.Api.Models;

namespace Web.Api;

public static class MappingExtensions
{
    public static Restaurant ToEntity(this RestaurantDto restaurantDto)
    {
        Restaurant restaurant = new ();
        restaurant.Id = restaurantDto.Id;
        restaurant.Name = restaurantDto.Name;
        restaurant.Description = restaurantDto.Description;
        restaurant.RestaurantCategory = restaurantDto.RestaurantCategory;
        restaurant.RestaurantCategoryId = restaurant.RestaurantCategory.Id;
        restaurant.Products = restaurantDto.Products;
        restaurant.Orders = restaurantDto.Orders;
        restaurant.Reviews = restaurantDto.Reviews;

        return restaurant;
    }

    public static RestaurantDto ToDto(this Restaurant restaurant)
    {
        RestaurantDto restaurantDto = new();
        restaurantDto.Id = restaurant.Id;
        restaurantDto.Name = restaurant.Name;
        restaurantDto.Description = restaurant.Description;
        restaurantDto.RestaurantCategory = restaurant.RestaurantCategory;
        restaurantDto.Products = restaurant.Products;
        restaurantDto.Orders = restaurant.Orders;
        restaurantDto.Reviews = restaurant.Reviews;

        return restaurantDto;
    }
}
