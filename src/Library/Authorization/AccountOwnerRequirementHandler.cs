using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Library.Authorization;

public class AccountOwnerRequirementHandler : AuthorizationHandler<AccountOwnerRequirement>
{
    protected async override Task HandleRequirementAsync(AuthorizationHandlerContext context, AccountOwnerRequirement requirement)
    {
        await Task.CompletedTask;

        if (context.User.IsInRole("admin"))
        {
            context.Succeed(requirement);
            return;
        }
        var nameIdentifire = context.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier);
  
        if(nameIdentifire?.Value == requirement.UserId.ToString())
        {
            context.Succeed(requirement);
        }
    }
}
