using System;
using System.Linq;
using LuxuryAPIv3.Data;
using System.Data.SqlClient;
using LuxuryAPIv3.Models.Revenue;
using System.Collections.Generic;

namespace LuxuryAPIv3.Adapters.Revenue
{
    public class OrderDetailsAdapter
    {
        // Define a list for Hair services
        private static List<OrderDetails> orderDetails = new List<OrderDetails>();
        // Define SQL connection 
        private static SqlConnection conn = DataContext.Connection;

        // Properties
        public static List<OrderDetails> ListOrderDetails
        {
            get => orderDetails;
            set => orderDetails = value;
        }

        // Method(s) that direct-interact with server
        public static double GetIncomeRevenue()
        {
            List<OrderDetails> fetchedData = FetchData();
            if (fetchedData != null)
            {
                return fetchedData.Sum(k => k.TotalPrice);
            }

            return 0;
        }
        public static List<OrderDetails> FetchData()
        {
            // Define a linked list
            List<OrderDetails> fetchList = new List<OrderDetails>();
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT * FROM OrderDetails;";
            // Build SQL Reader
            SqlDataReader reader = cmd.ExecuteReader();
            // Check if data has >= 1 row
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // Read fetched data
                    double TotalPrice = Convert.ToDouble(reader["TotalPrice"]);
                    int IDSvc = Convert.ToInt32(reader["IDSvc"]);
                    int IDOrder = Convert.ToInt32(reader["IDOrder"]);
                    // Add data to linked list 
                    fetchList.Add(new OrderDetails(TotalPrice, IDSvc, IDOrder));
                }
                // Assign fetched data to HairServices
                ListOrderDetails = fetchList;
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
        public static OrderDetails[] GetAll()
        {
            ListOrderDetails = FetchData();
            if (ListOrderDetails != null)
            {
                return ListOrderDetails.ToArray();
            }

            return null;
        }
        public static OrderDetails[] GetItem(int IDOrder)
        {
            ListOrderDetails = FetchData();
            if (ListOrderDetails != null)
            {
                return ListOrderDetails.Where(k => k.IDOrder == IDOrder).ToArray();
            }

            return null;
        }
        public static string InsertData(OrderDetails orderDetails)
        {
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO OrderDetails VALUES(@TotalPrice, @IDSvc, @IDOrder);";
            cmd.Parameters.AddWithValue("@TotalPrice", orderDetails.TotalPrice);
            cmd.Parameters.AddWithValue("@IDSvc", orderDetails.IDSvc);
            cmd.Parameters.AddWithValue("@IDOrder", orderDetails.IDOrder);
            // Build SQL Non-query executor
            int rows_affected = cmd.ExecuteNonQuery();
            string result = $"({rows_affected}) row(s) affected!";
            // Close SQL connection
            conn.Close();

            return result;
        }
    }
}