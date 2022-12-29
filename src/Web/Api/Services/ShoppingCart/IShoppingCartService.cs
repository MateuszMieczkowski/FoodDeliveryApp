using Library.Entities;
using Web.Api.Models.ShoppingCartDtos;

namespace Library.Services.ShoppingCart;

public interface IShoppingCartService
{
    Task AddToCartAsync(int productId);

    Task DeleteFromCartAsync(int productId);

    ShoppingCartDto GetShoppingCart();
}
