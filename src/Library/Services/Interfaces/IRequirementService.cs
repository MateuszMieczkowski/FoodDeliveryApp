using Microsoft.AspNetCore.Authorization;

namespace Library.Services.Interfaces;

public interface IRequirementService
{
    public Task<AuthorizationResult> AuthorizeAsync(IAuthorizationRequirement requirement);
}