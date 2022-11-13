using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.Restaurant;

public class StartModel : PageModel
{
    [BindProperty]
    [Required]
    [MaxLength(30)]
    public string City { get; set; } = string.Empty;

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        return RedirectToPage("Index", "City", new { city = City });
    }
}
