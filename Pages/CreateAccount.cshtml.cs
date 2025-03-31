using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CRM.Pages
{
    public class CreateAccountModel : PageModel
    {
        [BindProperty]
        public AccountModel NewAccount { get; set; }  // Renamed to AccountModel

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            if (NewAccount == null || string.IsNullOrEmpty(NewAccount.Username) || string.IsNullOrEmpty(NewAccount.Email) || string.IsNullOrEmpty(NewAccount.Password))
            {
                ErrorMessage = "All fields are required.";
                return;
            }

            try
            {
                string connectionString = "Data Source=JIGAR\\SQLEXPRESS;Initial Catalog=crm_system;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO account (username, email, password, created_at, role_id) VALUES (@Username, @Email, @Password, GETDATE(), 2)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", NewAccount.Username);
                        command.Parameters.AddWithValue("@Email", NewAccount.Email);
                        command.Parameters.AddWithValue("@Password", NewAccount.Password); // Store hashed password in real implementation

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            SuccessMessage = "Account created successfully!";
                        }
                        else
                        {
                            ErrorMessage = "Failed to create account.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred while creating the account: " + ex.Message;
            }
        }
    }

    public class AccountModel  // Changed class name to avoid conflicts
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
