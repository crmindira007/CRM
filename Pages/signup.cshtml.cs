using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace CRM.Pages
{
    public class signupModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Organization Name is required")]
        public string OrganizationName { get; set; }

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

        private readonly string _connectionString;

        public signupModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    // Check if the email already exists
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE email = @Email";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Email", Email);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            ModelState.AddModelError("Email", "This email is already registered.");
                            return Page();
                        }
                    }

                    // If email doesn't exist, insert new user
                    string insertQuery = "INSERT INTO users (first_name, organization_name, email, password) VALUES (@FullName, @OrganizationName, @Email, @Password)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@FullName", FullName);
                        cmd.Parameters.AddWithValue("@OrganizationName", OrganizationName);
                        cmd.Parameters.AddWithValue("@Email", Email);
                        cmd.Parameters.AddWithValue("@Password", Password); // Consider hashing before storing

                        cmd.ExecuteNonQuery();
                    }
                }

                TempData["SuccessMessage"] = "Signup successful!";
                return RedirectToPage("/signin");
            }
            catch (SqlException)
            {
                ModelState.AddModelError("", "An error occurred while processing your request.");
                return Page();
            }
        }
    }
}
