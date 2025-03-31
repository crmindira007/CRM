using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

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

        private readonly string _connectionString;

        public loginModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

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

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    string query = "SELECT password FROM users WHERE email = @Email";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", Email);

                        object result = cmd.ExecuteScalar();

                        if (result == null)
                        {
                            ErrorMessage = "Invalid email or password.";
                            return Page();
                        }

                        string storedPassword = result.ToString();

                        // Verify password
                        if (Password != storedPassword)
                        {
                            ErrorMessage = "Invalid email or password.";
                            return Page();
                        }
                    }
                }

                // Store session or authentication cookie (Optional)
                HttpContext.Session.SetString("UserEmail", Email);

                return RedirectToPage("/Dashboard"); // Redirect after successful login
            }
            catch (SqlException)
            {
                ErrorMessage = "An error occurred while processing your request.";
                return Page();
            }
        }

        // Hash verification (for later when you implement password hashing)
        private bool VerifyPassword(string inputPassword, string storedHash)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(inputPassword);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                string inputHash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                return inputHash == storedHash;
            }
        }
    }
}
