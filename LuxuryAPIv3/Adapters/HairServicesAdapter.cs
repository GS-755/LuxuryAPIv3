using System;
using System.Linq;
using LuxuryAPIv3.Data;
using LuxuryAPIv3.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace LuxuryAPIv3.Adapters
{
    public class HairServicesAdapter
    {
        // Define a list for Hair services
        private static List<HairService> hairServices = new List<HairService>();
        // Define SQL connection 
        private static SqlConnection conn = DataContext.Connection;

        // Properties
        public static List<HairService> HairServices
        {
            get => hairServices;
            set => hairServices = value;
        }

        // Method(s) that direct-interact with server
        public static int GetCurrentId()
        {
            List<HairService> fetchedData = FetchData();
            if(fetchedData != null)
            {
                return fetchedData.Max(k => k.IdSvc);
            }

            return -1;
        }
        public static List<HairService> FetchData()
        {
            // Define a linked list
            List<HairService> fetchList = new List<HairService>();
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT * FROM HairServices;";
            // Build SQL Reader
            SqlDataReader reader = cmd.ExecuteReader();
            // Check if data has >= 1 row
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // Read fetched data
                    int IdSvc = Convert.ToInt32(reader["IdSvc"]);
                    string Name = Convert.ToString(reader["Name"]);
                    double Price = Convert.ToDouble(reader["Price"]);
                    int IdCate = Convert.ToInt32(reader["IdCate"]);
                    // Add data to linked list 
                    fetchList.Add(new HairService(IdSvc, Name, Price, IdCate));
                }
                // Assign fetched data to HairServices
                HairServices = fetchList;
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
        public static HairService[] GetAll()
        {
            HairServices = FetchData();
            if(HairServices != null)
            {
                return HairServices.ToArray();
            }

            return null;
        }
        public static HairService[] GetItem(int IdSvc)
        {
            HairServices = FetchData();
            if (HairServices != null)
            {
                return HairServices.Where(k => k.IdSvc == IdSvc).ToArray();
            }

            return null;
        }
        public static string InsertData(HairService hairService)
        {
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO HairServices VALUES(@IdSvc, @Name, @Price, @IdCate);";
            cmd.Parameters.AddWithValue("@IdSvc", hairService.IdSvc);
            cmd.Parameters.AddWithValue("@Name", hairService.Name);
            cmd.Parameters.AddWithValue("@Price", hairService.Price);
            cmd.Parameters.AddWithValue("@IdCate", hairService.IdCate);
            // Build SQL Non-query executor
            int rows_affected = cmd.ExecuteNonQuery();
            string result = $"({rows_affected}) row(s) affected!";
            // Close SQL connection
            conn.Close();

            return result;
        }
        public static string UpdateData(HairService hairService)
        {
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                                UPDATE HairServices 
                                SET Name = @Name, 
                                    Price = @Price, 
                                    IdCate = @IdCate 
                                WHERE IdSvc = @IdSvc;
                               ";
            cmd.Parameters.AddWithValue("@Name", hairService.Name);
            cmd.Parameters.AddWithValue("@Price", hairService.Price);
            cmd.Parameters.AddWithValue("@IdCate", hairService.IdCate);
            cmd.Parameters.AddWithValue("@IdSvc", hairService.IdSvc);
            // Build SQL Non-query executor
            int rows_affected = cmd.ExecuteNonQuery();
            string result = $"({rows_affected}) row(s) affected!";
            // Close SQL connection
            conn.Close();

            return result;
        }
        public static string DeleteData(int IdSvc)
        {
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                                DELETE HairServices 
                                    WHERE IdSvc = @IdSvc;
                               ";
            cmd.Parameters.AddWithValue("@IdSvc", IdSvc);
            // Build SQL Non-query executor
            int rows_affected = cmd.ExecuteNonQuery();
            string result = $"({rows_affected}) row(s) affected!";
            // Close SQL connection
            conn.Close();

            return result;
        }
    }
}