using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Web.ViewModels;
using Library.Entities;
using Web.Api.Models.RestaurantDtos;

namespace Web.Pages.Restaurant;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;


    public HomeViewModel HomeViewModel { get; private set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> OnGet()
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync($"http://localhost:5007/api/restaurants");
        if (!response.IsSuccessStatusCode)
        {
            return RedirectToPage("Error");
        }
        string responseBody = await response.Content.ReadAsStringAsync();
        IEnumerable<RestaurantDto>? restaurants = JsonConvert.DeserializeObject<IEnumerable<RestaurantDto>>(responseBody);
        HomeViewModel = new HomeViewModel(restaurants);
        return Page();
    }
    public async Task<IActionResult> OnGetSearchQuery(string? searchQuery)
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync($"http://localhost:5007/api/restaurants?&searchQuery={searchQuery}");
        if (!response.IsSuccessStatusCode)
        {
            return RedirectToPage("Error");
        }
        string responseBody = await response.Content.ReadAsStringAsync();
        IEnumerable<RestaurantDto>? restaurants = JsonConvert.DeserializeObject<IEnumerable<RestaurantDto>>(responseBody);
        HomeViewModel = new HomeViewModel(restaurants);
        return Page();
    }

    public async Task<IActionResult> OnGetCity(string? city)
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync($"http://localhost:5007/api/restaurants?&city={city}");
        if (!response.IsSuccessStatusCode)
        {
            return RedirectToPage("Error");
        }
        string responseBody = await response.Content.ReadAsStringAsync();
        IEnumerable<RestaurantDto>? restaurants = JsonConvert.DeserializeObject<IEnumerable<RestaurantDto>>(responseBody);
        HomeViewModel = new HomeViewModel(restaurants);
        return Page();
    }
}