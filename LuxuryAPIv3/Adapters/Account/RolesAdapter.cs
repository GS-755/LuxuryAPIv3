using System;
using System.Linq;
using LuxuryAPIv3.Data;
using System.Data.SqlClient;
using LuxuryAPIv3.Models.Account;
using System.Collections.Generic;

namespace LuxuryAPIv3.Adapters.Account
{
    public class RolesAdapter
    {
        // Define a list for Hair services
        private static List<Role> listRole = new List<Role>();
        // Define SQL connection 
        private static SqlConnection conn = DataContext.Connection;

        // Properties
        public static List<Role> ListRole
        {
            get => listRole;
            set => listRole = value;
        }

        // Method(s) that direct-interact with server
        public static List<Role> FetchData()
        {
            // Define a linked list
            List<Role> fetchList = new List<Role>();
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT * FROM Roles;";
            // Build SQL Reader
            SqlDataReader reader = cmd.ExecuteReader();
            // Check if data has >= 1 row
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // Read fetched data
                    int IDRole = Convert.ToInt32(reader["IDRole"]);
                    string Name = Convert.ToString(reader["Name"]);
                    // Add data to linked list 
                    fetchList.Add(new Role(IDRole, Name));
                }
                // Close the reader & connection
                reader.Close();
                conn.Close();
                // Assign fetched data to ListStaff
                ListRole = fetchList;

                // Return data
                return fetchList;
            }
            // Close the reader & connection
            reader.Close();
            conn.Close();

            // Return null data
            return null;
        }

        // Method(s) for ListRole
        public static void Clear()
        {
            ListRole.Clear();
        }

        // API Function(s)
        public static Role[] GetAll()
        {
            ListRole = FetchData();
            if (ListRole != null)
            {
                return ListRole.ToArray();
            }

            return null;
        }
        public static Role[] GetItem(int IDRole)
        {
            ListRole = FetchData();
            if (ListRole != null)
            {
                return ListRole.Where(k => k.IDRole == IDRole).ToArray();
            }

            return null;
        }
    }
}