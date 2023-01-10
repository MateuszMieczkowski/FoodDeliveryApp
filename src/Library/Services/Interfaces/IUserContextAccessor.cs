using System.Security.Claims;

namespace Library.Services.Interfaces;

public interface IUserContextAccessor
{
    public ClaimsPrincipal? User { get; }
}