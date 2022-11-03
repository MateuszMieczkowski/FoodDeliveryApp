using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
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

 

    public async Task<IActionResult> OnGet()
    {
        var city = HttpContext?.Session?.GetString("City");
        if (city is null)
        {
            return RedirectToPage("Start");
        }

        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync("http://localhost:5007/api/restaurants");
        if (!response.IsSuccessStatusCode)
        {
            return RedirectToPage("Error");
        }
        string responseBody = await response.Content.ReadAsStringAsync();
        IEnumerable<RestaurantDto>? restaurants = JsonConvert.DeserializeObject<IEnumerable<RestaurantDto>>(responseBody);
        
        if(city is not null)
        {
            restaurants = restaurants?.Where(r => r.City == city);
        }
        HomeViewModel = new HomeViewModel(restaurants);
        return Page();
    }
}