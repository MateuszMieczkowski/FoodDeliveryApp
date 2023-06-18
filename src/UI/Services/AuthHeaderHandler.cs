using System.Net.Http.Headers;
using UI.Services.UserService;

namespace UI.Services;

class AuthHeaderHandler : DelegatingHandler
{
    private readonly IUserService _userService;
    public AuthHeaderHandler(IUserService userService) : base()
    {
        _userService = userService;
        InnerHandler = new HttpClientHandler();
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _userService.User?.Token;

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}
