using System;
using System.Linq;
using LuxuryAPIv3.Data;
using LuxuryAPIv3.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace LuxuryAPIv3.Adapters
{
    public class CategoryAdapter
    {
        // Init connection from Context 
        private static SqlConnection conn = DataContext.Connection;
        // Init linked list for Category
        private static List<Category> listCate = new List<Category>();

        // Properties
        public static List<Category> ListCate
        {
            get { return listCate; }
            set { listCate = value; }
        }

        public static List<Category> FetchData()
        {
            // Define a linked list
            List<Category> fetchList = new List<Category>();
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT * FROM Category;";
            // Build SQL Reader
            SqlDataReader reader = cmd.ExecuteReader();
            // Check if data has >= 1 row
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // Read fetched data
                    int IdCate = Convert.ToInt32(reader["IdCate"]);
                    string Name = Convert.ToString(reader["Name"]);
                    string Image = Convert.ToString(reader["Image"]);
                    // Add data to linked list 
                    fetchList.Add(new Category(IdCate, Name, Image));
                }
                // Close the reader & connection
                reader.Close();
                conn.Close();
                // Assign fetched data to ListCate
                ListCate = fetchList;

                // Return data
                return fetchList;
            }
            // Close the reader & connection
            reader.Close();
            conn.Close();

            // Return null data
            return null;
        }

        //Method(s) for cateList
        public static void AddItem(Category category)
        {
            listCate.Add(category);
        }
        // API Function(s)
        public static Category[] GetAll()
        {
            ListCate = FetchData();
            if(ListCate != null)
            {
                return ListCate.ToArray();
            }

            return null;
        }
        public static Category[] GetItem(int IdCate)
        {
            ListCate = FetchData();
            if (ListCate != null)
            {
                return ListCate.Where(k => k.IdCate == IdCate).ToArray();
            }

            return null;
        }
        public static string InsertData(Category category)
        {
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"INSERT INTO Category VALUES(@Name);";
            cmd.Parameters.AddWithValue("@Name", category.Name);
            // Build SQL Non-query executor
            int rows_affected = cmd.ExecuteNonQuery();
            string result = $"({rows_affected}) row(s) affected!";
            // Close SQL connection
            conn.Close();

            return result;
        }
        public static string UpdateData(Category category)
        {
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"UPDATE Category SET Name = @Name WHERE IdCate = @IdCate;";
            cmd.Parameters.AddWithValue("@Name", category.Name);
            cmd.Parameters.AddWithValue("@IdCate", category.IdCate);
            // Build SQL Non-query executor
            int rows_affected = cmd.ExecuteNonQuery();
            string result = $"({rows_affected}) row(s) affected!";
            // Close SQL connection
            conn.Close();

            return result;
        }
        public static string DeleteData(int IdCate)
        {
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"DELETE Category WHERE IdCate = @IdCate;";
            cmd.Parameters.AddWithValue("@IdCate", IdCate);
            // Build SQL Non-query executor
            int rows_affected = cmd.ExecuteNonQuery();
            string result = $"({rows_affected}) row(s) affected!";
            // Close SQL connection
            conn.Close();

            return result;
        }
    }
}