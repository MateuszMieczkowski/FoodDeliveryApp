using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using UI.Services.UserService;
using Microsoft.AspNetCore.Authorization;

namespace UI;

public class AppRouteView : RouteView
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IUserService UserService { get; set; }

    protected override void Render(RenderTreeBuilder builder)
    {
        var authorizeAttribute = Attribute.GetCustomAttribute(RouteData.PageType, typeof(AuthorizeAttribute)) as AuthorizeAttribute;
        var requiredRole = authorizeAttribute?.Roles;
        var isAuthenticated = UserService.IsAuthenticated;
        string userRole = string.Empty;
        if(isAuthenticated)
        {
            userRole = UserService!.User!.Role;
        }

        if (authorizeAttribute is not null && !isAuthenticated)
        {
            NavigationManager.NavigateTo("/login");
        }
        else if(requiredRole is not null && isAuthenticated && !requiredRole.Contains(userRole))
        {
            NavigationManager.NavigateTo("/forbidden");
        }
        else
        {
            base.Render(builder);
        }
    }
}
