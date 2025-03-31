using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CRM.Pages
{
    public class AccountsModel : PageModel
    {
        public List<Account> Accounts { get; set; } = new List<Account>();

        public void OnGet()
        {
            string connectionString = "Data Source=JIGAR\\SQLEXPRESS;Initial Catalog=crm_system;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT username, email, created_at FROM account";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Accounts.Add(new Account
                            {
                                Username = reader["username"].ToString(),
                                Email = reader["email"].ToString(),
                                CreatedAt = reader["created_at"].ToString()
                            });
                        }
                    }
                }
            }
        }
    }

    public class Account
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string CreatedAt { get; set; }
    }
}