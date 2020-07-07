using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AlarmSystem
{
    public static class Database
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["SCADAConnectionString"].ConnectionString;

        public static void executeQuery(string query)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }

        public static DataTable getTablefromDB(string query)
        {
            DataTable table = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                SqlDataAdapter adapt = new SqlDataAdapter(query, conn);
                conn.Open();
                adapt.Fill(table);
                conn.Close();

                return table;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw ex;
            }
        }
    }
}