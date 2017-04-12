using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PerfectInProcess.Models
{
    public class Home
    {

        public static ArrayList listOfErrors = new ArrayList();

        public int VisitorCount { get; set; }


        public void SetVisitorCount()
        {
            SqlConnection connection = new SqlConnection(Convert.ToString(ConfigurationManager.ConnectionStrings["DefaultConnection"]));

            try
            {
                using (SqlCommand command = new SqlCommand("SetVisitorCount", connection))
                {
                    command.Parameters.Add("@VisitorCount", SqlDbType.Int).Value = VisitorCount;                   
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    command.ExecuteNonQuery(); 
                }
            }
            catch (SqlException ex)
            {
                listOfErrors.Add(ex.Message);
            }

        }


        public void GetVisitorCount()
        {
            SqlConnection connection = new SqlConnection(Convert.ToString(ConfigurationManager.ConnectionStrings["DefaultConnection"]));

            try
            {
                using (SqlCommand command = new SqlCommand("GetVisitorCount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;                   
                    command.Parameters.Add("@VisitorCount", SqlDbType.Int).Direction = ParameterDirection.Output; ;
                    connection.Open();
                    command.ExecuteNonQuery();

                    VisitorCount = (int)command.Parameters["@VisitorCount"].Value;
                }
            }
            catch (SqlException ex)
            {
                listOfErrors.Add(ex.Message);
            }

        }




    }
}