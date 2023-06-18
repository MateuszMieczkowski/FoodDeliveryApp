namespace UI.Services.UserService;

public interface IUserService
{
    User? User { get; }

    bool IsAuthenticated { get; }
    Task LoginAsync(string email, string password);

    Task SignOutAsync();

    Task<Guid> GetUserIdAsync();

    Task<string> GetUserRoleAsync();
}
