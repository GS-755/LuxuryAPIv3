using System;
using System.Linq;
using LuxuryAPIv3.Data;
using LuxuryAPIv3.Models;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace LuxuryAPIv3.Adapters
{
    public class StaffAdapter
    {
        // Init connection from Context 
        private static SqlConnection conn = DataContext.Connection;
        // Init linked list for Category
        private static List<Staff> listStaff = new List<Staff>();

        // Properties
        public static List<Staff> ListStaff
        {
            get { return listStaff; }
            set { listStaff = value; }
        }

        // Method(s) that direct-interact with server
        public static int GetCurrentId()
        {
            return FetchData().Max(k => k.IDStaff);
        }
        public static List<Staff> FetchData()
        {
            // Define a linked list
            List<Staff> fetchList = new List<Staff>();
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"SELECT * FROM Staff;";
            // Build SQL Reader
            SqlDataReader reader = cmd.ExecuteReader();
            // Check if data has >= 1 row
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // Read fetched data
                    Staff staff = new Staff();
                    staff.IDStaff = Convert.ToInt32(reader["IDStaff"]);
                    staff.FName = Convert.ToString(reader["FName"]);
                    staff.LName = Convert.ToString(reader["LName"]);
                    staff.DOB = Convert.ToString(reader["DOB"]);
                    staff.Phone = Convert.ToString(reader["Phone"]);
                    staff.Address = Convert.ToString(reader["Address"]);
                    staff.CurrentSalary = Convert.ToDouble(reader["CurrentSalary"]);
                    // Add data to linked list 
                    fetchList.Add(staff);
                }
                // Close the reader & connection
                reader.Close();
                conn.Close();
                // Assign fetched data to ListStaff
                ListStaff = fetchList;

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
        public static void Clear()
        {
            ListStaff.Clear();
        }
        // API Function(s)
        public static Staff[] GetAll()
        {
            ListStaff = FetchData();
            if (ListStaff != null)
            {
                return ListStaff.ToArray();
            }

            return null;
        }
        public static Staff[] GetItem(int IDStaff)
        {
            ListStaff = FetchData();
            if (ListStaff != null)
            {
                return ListStaff.Where(k => k.IDStaff == IDStaff).ToArray();
            }

            return null;
        }
        public static string InsertData(Staff staff)
        {
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                                INSERT INTO Staff 
                                    VALUES (
                                        @IDStaff, 
                                        @FName,
                                        @LName,
                                        @DOB,
                                        @Phone,
                                        @Address,
                                        @CurrentSalary
                                    );
                               ";
            cmd.Parameters.AddWithValue("@IDStaff", staff.IDStaff);
            cmd.Parameters.AddWithValue("@FName", staff.FName);
            cmd.Parameters.AddWithValue("@LName", staff.LName);
            cmd.Parameters.AddWithValue("@DOB", staff.DOB);
            cmd.Parameters.AddWithValue("@Phone", staff.Phone);
            cmd.Parameters.AddWithValue("@Address", staff.Address);
            cmd.Parameters.AddWithValue("@CurrentSalary", staff.CurrentSalary);
            // Build SQL Non-query executor
            int rows_affected = cmd.ExecuteNonQuery();
            string result = $"({rows_affected}) row(s) affected!";
            // Close SQL connection
            conn.Close();

            return result;
        }
        public static string UpdateData(Staff staff)
        {
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"
                                UPDATE Staff 
                                    SET IDStaff = @IDStaff, 
                                        FName = @FName,
                                        LName = @LName,
                                        DOB = @DOB,
                                        Phone = @Phone,
                                        Address = @Address,
                                        CurrentSalary = @CurrentSalary 
                                WHERE IDStaff = @IDStaff;";
            cmd.Parameters.AddWithValue("@IDStaff", staff.IDStaff);
            cmd.Parameters.AddWithValue("@FName", staff.FName);
            cmd.Parameters.AddWithValue("@LName", staff.LName);
            cmd.Parameters.AddWithValue("@DOB", staff.DOB);
            cmd.Parameters.AddWithValue("@Phone", staff.Phone);
            cmd.Parameters.AddWithValue("@Address", staff.Address);
            cmd.Parameters.AddWithValue("@CurrentSalary", staff.CurrentSalary);
            // Build SQL Non-query executor
            int rows_affected = cmd.ExecuteNonQuery();
            string result = $"({rows_affected}) row(s) affected!";
            // Close SQL connection
            conn.Close();

            return result;
        }
        public static string DeleteData(int IDStaff)
        {
            // Open connection
            conn.Open();
            // Build SQL Executor
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = @"DELETE Staff WHERE IDStaff = @IDStaff;";
            cmd.Parameters.AddWithValue("@IDStaff", IDStaff);
            // Build SQL Non-query executor
            int rows_affected = cmd.ExecuteNonQuery();
            string result = $"({rows_affected}) row(s) affected!";
            // Close SQL connection
            conn.Close();

            return result;
        }
    }
}