using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Web.Api.Models.RestaurantDtos;
using Web.ViewModels;

namespace Web.Pages.Restaurant;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;


    public HomeViewModel HomeViewModel { get; private set; }

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public IActionResult OnGet()
    {

        return RedirectToPage("Start");
    }

    public async Task<IActionResult> OnGetCity(string city)
    {
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync("http://localhost:5007/api/restaurant");
        if (!response.IsSuccessStatusCode)
        {
            return RedirectToPage("Error");
        }
        string responseBody = await response.Content.ReadAsStringAsync();
        var restaurants = JsonConvert.DeserializeObject<IEnumerable<RestaurantDto>>(responseBody)?.Where(r=>r.City.ToLower() == city.ToLower());
        HomeViewModel = new HomeViewModel(restaurants);
        return Page();
    }
}