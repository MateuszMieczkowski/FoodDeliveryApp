using Refit;
using UI.API.Requests;
using UI.API.Responses;

namespace UI.API;

public interface IAuthApi
{
    [Post("/auth/login")]
    Task<string> LoginAsync([Body]LoginRequest login);

    [Post("/auth/register")]
    Task RegisterUserAsync([Body] RegisterRequest registerUser);
}
