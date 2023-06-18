namespace UI.Services.LocalStorage;

public interface ILocalStorageAccessor : IAsyncDisposable
{
    Task SetJwtTokenAsync(string token);

    Task<string> GetJwtTokenAsync();

    Task ClearJwtTokenAsync();

    Task<T> GetValueAsync<T>(string key);

    Task SetValueAsync<T>(string key, T value);

    Task Clear();

    Task RemoveAsync(string key);
}
