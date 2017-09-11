using System.Data.SqlClient;
using System.Configuration;

namespace CurbsideTestProject.DataAccess
{
    public class DBConnection
    {
        string connectionString = ConfigurationManager.AppSettings["connectionstring"];
        public int GetUserCount()
        {
            string tenantId = ConfigurationManager.AppSettings["TenantId"];
            string selectUsers = "select count(*) from [dbo].[User] where TenantID = '"+ tenantId + "'"; 
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand Userscmd = new SqlCommand(selectUsers, con);
                con.Open();
                int numrows = (int)Userscmd.ExecuteScalar();
                return numrows;
            }
            
        }

    }
}