using System.ComponentModel.DataAnnotations;

namespace Library.Models.OrderDtos;

public record OrderCreateDto(Guid UserId, int RestaurantId, AddressDto? Address);