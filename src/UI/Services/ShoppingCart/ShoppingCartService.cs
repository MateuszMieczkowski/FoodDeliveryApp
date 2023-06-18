using UI.API;
using UI.API.Responses;
using UI.Services.LocalStorage;

namespace UI.Services.ShoppingCart;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartApi _shoppingCartApi;
    private readonly ILocalStorageAccessor _localStorageAccessor;

    public ShoppingCartService(IShoppingCartApi shoppingCartApi, ILocalStorageAccessor localStorageAccessor)
    {
        _shoppingCartApi = shoppingCartApi;
        _localStorageAccessor = localStorageAccessor;
    }

    public async Task Add(int productId)
    {
        var shoppingCartId = await GetShoppingCartId();
        await _shoppingCartApi.AddToCartAsync(productId, shoppingCartId);
    }

    public async Task Delete(int productId)
    {
        var shoppingCartId = await GetShoppingCartId();
        await _shoppingCartApi.DeleteFromCartAsync(productId, shoppingCartId);
    }

    public async Task<UI.API.Responses.ShoppingCart> GetCartAsync(int restaurantId)
    {
        var shoppingCartId = await GetShoppingCartId();
        return await _shoppingCartApi.GetCartAsync(restaurantId, shoppingCartId);
    }

    private async Task<Guid> GetShoppingCartId()
    {
        var shoppingCartId = await _localStorageAccessor
            .GetValueAsync<Guid?>("shoppingCartId");
        if(shoppingCartId is null) 
        {
            shoppingCartId = new Guid();
            await _localStorageAccessor.SetValueAsync("shoppingCartId", shoppingCartId.Value);
        }
        return shoppingCartId.Value;
    }
}
