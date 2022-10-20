using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages
{
    public class StartModel : PageModel
    {
        [BindProperty]
        [Required]
        [MaxLength(10)]
        public string City { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
           if(!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("Index", "City",City);
        }
    }
}
