using UI.API.Responses;

namespace UI.ViewModels;

public class RestaurantDetailsViewModel
{
	public Restaurant Restaurant { get; set; } = default!;

	public List<Product> Products { get; set; } = default!;
}
