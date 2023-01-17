using Microsoft.AspNetCore.Authorization;

namespace Library.Authorization;

public class AccountOwnerRequirement : IAuthorizationRequirement
{
    public Guid UserId { get; }
	public AccountOwnerRequirement(Guid userId)
	{
		UserId = userId;
	}
}
