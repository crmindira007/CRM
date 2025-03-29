using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRM.Pages
{
    public class loginModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters.")]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            ErrorMessage = string.Empty;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Show validation errors
            }

            // Simulated authentication logic
            if (Email != "admin@example.com" || Password != "admin123")
            {
                ErrorMessage = "Invalid email or password.";
                return Page(); // Stay on the page and display error
            }

            return RedirectToPage("/Dashboard"); // Redirect after successful login
        }
    }
}
