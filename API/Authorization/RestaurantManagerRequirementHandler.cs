using Library.Entities;
using Microsoft.AspNetCore.Authorization;

namespace API.Authorization;

public class RestaurantManagerRequirementHandler : AuthorizationHandler<RestaurantManagerRequirement, Restaurant>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RestaurantManagerRequirement requirement,
        Restaurant resource)
    {
        await Task.CompletedTask;
        if (context.User.IsInRole("admin"))
        {
            context.Succeed(requirement);
        }

        if (context.User.IsInRole("manager"))
        {
            var managedRestaurantId = context.User.FindFirst(c=> c.Type == "ManagedRestaurantId")?.Value;
            if (managedRestaurantId == resource.Id.ToString())
            {
                context.Succeed(requirement);
            }
        }

       
    }
}