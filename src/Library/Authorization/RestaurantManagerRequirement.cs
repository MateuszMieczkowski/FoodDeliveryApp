using Microsoft.AspNetCore.Authorization;

namespace Library.Authorization;

public class RestaurantManagerRequirement : IAuthorizationRequirement
{
    public int RestaurantId { get; }
	public RestaurantManagerRequirement(int restaurantId)
	{
		RestaurantId = restaurantId;
	}
}