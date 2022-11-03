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
    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        HttpContext?.Session?.SetString("City", City);
        return RedirectToPage("Index");
    }
}
