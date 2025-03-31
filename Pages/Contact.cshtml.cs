using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace CRM.Pages
{
    public class ContactModel : PageModel
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
                    String sql = "SELECT * FROM contact";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ContactInfo Contact = new ContactInfo();
                                Contact.id = reader.GetInt32(0);
                                Contact.Full_name = reader.GetString(1);
                                Contact.phone = reader.GetString(2);

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
        public class ContactInfo
        {
            public int id;
            public string Full_name;
            public string phone;
        }
    }
}
