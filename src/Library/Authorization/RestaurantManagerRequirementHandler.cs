using Library.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Library.Authorization;

public class RestaurantManagerRequirementHandler : AuthorizationHandler<RestaurantManagerRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RestaurantManagerRequirement requirement)
    {
        await Task.CompletedTask;
        if (context.User.IsInRole("admin"))
        {
            context.Succeed(requirement);
            return;
        }

        if (context.User.IsInRole("manager"))
        {
            var managedRestaurantId = context.User.FindFirst(c=> c.Type == "ManagedRestaurantId")?.Value;
            if (managedRestaurantId == requirement.RestaurantId.ToString())
            {
                context.Succeed(requirement);
            }
        }
    }
}