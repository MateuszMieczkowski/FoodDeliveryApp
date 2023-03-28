using Library.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Library.Services;

public class RequirementService : IRequirementService
{
    private readonly IUserContextAccessor _userContextAccessor;
    private readonly IAuthorizationService _authorizationService;

    public RequirementService(IUserContextAccessor userContextAccessor, IAuthorizationService authorizationService)
    {
        _userContextAccessor = userContextAccessor;
        _authorizationService = authorizationService;
    }

    public async Task<AuthorizationResult> AuthorizeAsync(IAuthorizationRequirement requirement)
    {
        var user = _userContextAccessor.User ?? throw new Exception();
		var authResult = await _authorizationService.AuthorizeAsync(user, null, requirement);
        return authResult;
    }
}