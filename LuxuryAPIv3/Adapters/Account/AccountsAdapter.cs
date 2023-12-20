using System;
using System.Linq;
using LuxuryAPIv3.Data;
using System.Data.SqlClient;
using LuxuryAPIv3.Models.Utils;
using System.Collections.Generic;
using LuxuryAPIv3.Models.Account;

namespace LuxuryAPIv3.Adapters.Account
{
    public class AccountsAdapter
    {
        // Define a list for Hair services
        private static List<Accounts> accounts = new List<Accounts>();
        // Define SQL connection 
        private static SqlConnection conn = DataContext.Connection;

        // Properties
        public static List<Accounts> ListAccount
        {
            get => accounts;
            set => accounts = value;
        }

        // Method(s) that direct-interact with server
        public static List<Accounts> FetchData()
        {
            // Define a linked list
            List<Accounts> fetchList = new List<Accounts>();
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT * FROM Account;";
            // Build SQL Reader
            SqlDataReader reader = cmd.ExecuteReader();
            // Check if data has >= 1 row
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // Read fetched data
                    Accounts account = new Accounts();
                    account.UserName = Convert.ToString(reader["UserName"]);
                    account.Password = Convert.ToString(reader["Password"]);
                    account.IDRole = Convert.ToInt32(reader["IDRole"]);
                    account.IDStaff = Convert.ToInt32(reader["IDStaff"]);
                    // Add data to linked list 
                    fetchList.Add(account);
                }
                // Assign fetched data to HairServices
                ListAccount = fetchList;
                // Close the reader & connection
                reader.Close();
                conn.Close();

                // Return data
                return fetchList;
            }
            // Close the reader & connection
            reader.Close();
            conn.Close();

            return null;
        }

        // API Function(s)
        public static Accounts[] GetAll()
        {
            ListAccount = FetchData();
            if (ListAccount != null)
            {
                return ListAccount.ToArray();
            }

            return null;
        }
        public static Accounts[] GetItem(string UserName)
        {
            ListAccount = FetchData();
            if (ListAccount != null)
            {
                return ListAccount.Where(k => k.UserName == UserName.Trim()).ToArray();
            }

            return null;
        }
        public static string InsertData(Accounts account)
        {
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO Account VALUES(@UserName, @Password, @IDRole, @IDStaff);";
            cmd.Parameters.AddWithValue("@UserName", account.UserName.Trim());
            cmd.Parameters.AddWithValue("@Password", SHA256.ToSHA256(account.Password));
            cmd.Parameters.AddWithValue("@IDRole", account.IDRole);
            cmd.Parameters.AddWithValue("@IDStaff", account.IDStaff);
            // Build SQL Non-query executor
            int rows_affected = cmd.ExecuteNonQuery();
            string result = $"({rows_affected}) row(s) affected!";
            // Close SQL connection
            conn.Close();

            return result;
        }
        public static string UpdateData(Accounts account)
        {
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                                UPDATE HairServices 
                                SET Password = @Password, 
                                    IDRole = @IDRole, 
                                    IDStaff = @IDStaff 
                                WHERE UserName = @UserName;
                               ";
            cmd.Parameters.AddWithValue("@Password", SHA256.ToSHA256(account.Password));
            cmd.Parameters.AddWithValue("@IDRole", account.IDRole);
            cmd.Parameters.AddWithValue("@IDStaff", account.IDStaff);
            cmd.Parameters.AddWithValue("@UserName", account.UserName.Trim());
            // Build SQL Non-query executor
            int rows_affected = cmd.ExecuteNonQuery();
            string result = $"({rows_affected}) row(s) affected!";
            // Close SQL connection
            conn.Close();

            return result;
        }
    }
}