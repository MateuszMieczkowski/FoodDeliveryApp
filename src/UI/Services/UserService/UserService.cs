using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UI.API;
using UI.Services.LocalStorage;

namespace UI.Services.UserService;

public class UserService : AuthenticationStateProvider, IUserService
{
    private readonly ILocalStorageAccessor _localStorageAccessor;
    private readonly IAuthApi _authApi;
    public User? User { get; private set; }

    public bool IsAuthenticated
    {
        get
        {
            if (User is null)
            {
                return false;
            }
            return User.ValidTo >= DateTime.UtcNow;
        }
    }

    public UserService
        (ILocalStorageAccessor localStorageAccessor,
        IAuthApi authApi)
    {
        _localStorageAccessor = localStorageAccessor;
        _authApi = authApi;
    }

    private async Task<JwtSecurityToken> GetJwtTokenAsync()
    {
        var token = await _localStorageAccessor.GetJwtTokenAsync();
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtTokenHandler.ReadJwtToken(token);
        return jwtToken;
    }

    public async Task<Guid> GetUserIdAsync()
    {
        var jwtToken = await GetJwtTokenAsync();
        var idClaim = jwtToken
            .Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        return idClaim is null ? Guid.Empty : Guid.Parse(idClaim.Value);
    }


    public async Task<string> GetUserRoleAsync()
    {
        var jwtToken = await GetJwtTokenAsync();
        var roleClaim = jwtToken
            .Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.Role);

        return roleClaim is null ? string.Empty : roleClaim.Value;
    }

    public async Task LoginAsync(string email, string password)
    {
        var loginRequest = new API.Requests.LoginRequest(email, password);
        var token = await _authApi.LoginAsync(loginRequest);
        await _localStorageAccessor.SetJwtTokenAsync(token);
        var jwt = await GetJwtTokenAsync();
        AssignUser(jwt);
    }

    private void AssignUser(JwtSecurityToken jwt)
    {
        var id = jwt
           .Claims
           .First(x => x.Type == ClaimTypes.NameIdentifier)
           .Value;
        var name = jwt
           .Claims
           .First(x => x.Type == ClaimTypes.Name)
           .Value;
        var role = jwt
           .Claims
           .First(x => x.Type == ClaimTypes.Role)
           .Value;
        User = new User(Guid.Parse(id), name, role, jwt.ValidTo, new JwtSecurityTokenHandler().WriteToken(jwt));
    }

    public async Task SignOutAsync()
    {
        await _localStorageAccessor.ClearJwtTokenAsync();
        User = null;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var jwtToken = await GetJwtTokenAsync();
        var claims = jwtToken.Claims;
        var claimsIdentity = new ClaimsIdentity(claims);
        var user = new ClaimsPrincipal(claimsIdentity);
        var state = new AuthenticationState(user);
        return state;
    }
}

public record User(Guid Id, string Name, string Role, DateTime ValidTo, string Token);
public static class UserRoles
{
    public const string User = "user";
    public const string Manager = "manager";
    public const string Admin = "admin";
}
