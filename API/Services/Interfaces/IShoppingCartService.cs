using Library.Entities;
using Web.Api.Models.ShoppingCartDtos;

namespace Web.Api.Services.Interfaces;

public interface IShoppingCartService
{
    Task AddToCartAsync(int productId);

    Task DeleteFromCartAsync(int productId);

    ShoppingCartDto GetShoppingCart();
}
