using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CRM.Pages
{
    public class signupModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page(); // Return the form with validation errors
            }

            // Process the valid form data (e.g., save to database)
            TempData["SuccessMessage"] = "Signup successful!";
            return RedirectToPage("/signin"); // Redirect to the sign-in page after success
        }
    }
}
