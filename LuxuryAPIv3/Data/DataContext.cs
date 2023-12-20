using System.Configuration;
using System.Data.SqlClient;

namespace LuxuryAPIv3.Data
{
    public class DataContext
    {
        public static string GetConnectionString()
        {
            string destination, username, password, db_name;
            destination = ConfigurationManager.AppSettings["HOST_URI"];
            username = ConfigurationManager.AppSettings["HOST_USRNAME"];
            password = ConfigurationManager.AppSettings["HOST_PASSWORD"];
            db_name = ConfigurationManager.AppSettings["DB_NAME"];

            return $"Data Source={destination};Initial Catalog={db_name};User ID={username};Password={password}";
        }
        public static SqlConnection Connection
        {
            get
            {
                SqlConnection sqlConnection = new SqlConnection(GetConnectionString());

                return sqlConnection;
            }
        }
    }
}