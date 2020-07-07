using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using DataEngine;
using System.Configuration;

namespace DataEngine
{
    class SQLhandler
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["SCADAConnectionString"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionString);
        double setpoint;

        public void LogData(double temp, double unfiltered, double signal, string status)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("DataLogging", connection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@Temperature", temp));
                cmd.Parameters.Add(new SqlParameter("@UnfilteredTemp", unfiltered));
                cmd.Parameters.Add(new SqlParameter("@Signal", signal));
                cmd.Parameters.Add(new SqlParameter("@Status", status));

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public double GetSetpoint()
        {
            try
            {
                connection.Close();
                
                SqlCommand cmd = new SqlCommand("SELECT Setpoint FROM LastSetpoint", connection);

                if(connection.State.ToString() != "Open")
                {
                    connection.Open();
                }

                using (SqlDataReader result = cmd.ExecuteReader())
                {
                    while (result.Read())
                    {
                        setpoint = result.GetDouble(0);
                    }
                }
                connection.Close();

                return setpoint;
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
    }
}