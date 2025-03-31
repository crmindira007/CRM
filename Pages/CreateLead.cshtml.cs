using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace CRM.Pages
{
    public class CreateLeadModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                string ConnectionString = "Data Source=RITESH\\SQLEXPRESS;Initial Catalog=crm_system;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM lead";
                }
            }
            catch
            {

            }
        }
    }
    public class ClientInfo
    {
        public int id;
    }
}
