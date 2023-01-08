using System.Security.Claims;

namespace API.Services.Interfaces;

public interface IUserContextAccessor
{
    public ClaimsPrincipal? User { get; }
}