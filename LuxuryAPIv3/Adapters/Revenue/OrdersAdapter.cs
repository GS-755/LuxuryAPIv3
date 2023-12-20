using System;
using System.Linq;
using LuxuryAPIv3.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using LuxuryAPIv3.Models.Revenue;

namespace LuxuryAPIv3.Adapters.Revenue
{
    public class OrdersAdapter
    {
        // Define a list for Hair services
        private static List<Orders> listOrders = new List<Orders>();
        // Define SQL connection 
        private static SqlConnection conn = DataContext.Connection;

        // Properties
        public static List<Orders> ListOrders
        {
            get => listOrders;
            set => listOrders = value;
        }

        // Method(s) that direct-interact with server
        public static int GetCurrentId()
        {
            List<Orders> fetchedData = FetchData();
            if (fetchedData != null)
            {
                return fetchedData.Max(k => k.IDOrder);
            }

            return -1;
        }
        public static List<Orders> FetchData()
        {
            // Define a linked list
            List<Orders> fetchList = new List<Orders>();
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT * FROM Orders;";
            // Build SQL Reader
            SqlDataReader reader = cmd.ExecuteReader();
            // Check if data has >= 1 row
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // Read fetched data
                    int IDOrder = Convert.ToInt32(reader["IDOrder"]);
                    string DateOrder = Convert.ToString(reader["DateOrder"]);
                    int IDStaff = Convert.ToInt32(reader["IDStaff"]);
                    // Add data to linked list 
                    fetchList.Add(new Orders(IDOrder, DateOrder, IDStaff));
                }
                // Assign fetched data to HairServices
                ListOrders = fetchList;
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
        public static Orders[] GetAll()
        {
            ListOrders = FetchData();
            if (ListOrders != null)
            {
                return ListOrders.ToArray();
            }

            return null;
        }
        public static Orders[] GetItem(int IDOrder)
        {
            ListOrders = FetchData();
            if (ListOrders != null)
            {
                return ListOrders.Where(k => k.IDOrder == IDOrder).ToArray();
            }

            return null;
        }
        public static string InsertData(Orders order)
        {
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO Orders VALUES(@IDOrder, @DateOrder, @IDStaff);";
            cmd.Parameters.AddWithValue("@IDOrder", order.IDOrder);
            cmd.Parameters.AddWithValue("@DateOrder", order.DateOrder);
            cmd.Parameters.AddWithValue("@IDStaff", order.IDStaff);
            // Build SQL Non-query executor
            int rows_affected = cmd.ExecuteNonQuery();
            string result = $"({rows_affected}) row(s) affected!";
            // Close SQL connection
            conn.Close();

            return result;
        }
        public static string DeleteData(int IDOrder)
        {
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                                DELETE Orders 
                                    WHERE IDOrder = @IDOrder;
                               ";
            cmd.Parameters.AddWithValue("@IDOrder", IDOrder);
            // Build SQL Non-query executor
            int rows_affected = cmd.ExecuteNonQuery();
            string result = $"({rows_affected}) row(s) affected!";
            // Close SQL connection
            conn.Close();

            return result;
        }
    }
}