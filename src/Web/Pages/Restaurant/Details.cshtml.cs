using Library.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Web.ViewModels;

namespace Web.Pages.Restaurant
{
    public class DetailsModel : PageModel
    {
        public Library.Entities.Restaurant Restaurant { get; private set; }
        public async Task<IActionResult> OnGet(int restaurantId)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"http://localhost:5007/api/restaurants/{restaurantId}");
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToPage("Error");
            }
            string responseBody = await response.Content.ReadAsStringAsync();
            Restaurant = JsonConvert.DeserializeObject<Library.Entities.Restaurant>(responseBody);
            return Page();
        }
    }
}
