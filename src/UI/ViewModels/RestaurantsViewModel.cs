using Microsoft.AspNetCore.Components;
using UI.API.Responses;

namespace UI.ViewModels;

public class RestaurantsViewModel
{
    public int PageSize { get; set; } = 10;

    public string? SearchQuery { get; set; }

    public string? City { get; set; }

    public string? RestaurantCategoryName { get; set; }

    public PagedResult<Restaurant>? PagedResult { get; set; }
}
