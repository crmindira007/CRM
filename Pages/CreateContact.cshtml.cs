using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CRM.Pages
{
    public class CreateContactModel : PageModel
    {
        public List<ContactInfo> listContact = new List<ContactInfo>();
        public void OnGet()
        {
            try
            {
                string ConnectionString = "Data Source=RITESH\\SQLEXPRESS;Initial Catalog=crm_system;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM conatct";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ContactInfo Contact = new ContactInfo();
                                Contact.id = reader.GetInt32(0);

                                listContact.Add(Contact);
                            }

                        }
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
        
    }
    public class ContactInfo
    {
        public int id;
    }
}
