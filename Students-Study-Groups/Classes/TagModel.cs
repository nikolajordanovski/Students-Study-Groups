using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Students_Study_Groups.Classes
{
    public class TagModel
    {
        public int TID               { get; set; }
        public string Name           { get; set; }
        public int NumberOfQuestions { get; set; }

        public TagModel(int tid)
        {
            TID = tid;
            GetTagsData(TID);
        }

        private void GetTagsData(int tid)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["SSG"].ConnectionString;
                try
                {
                    conn.Open();
                    SqlDataReader reader;
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;

                    command.CommandText = "SELECT * FROM Tags WHERE TID = @TID";
                    command.Parameters.AddWithValue("@TID", tid);
                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        Name              = reader["Name"].ToString();
                        NumberOfQuestions = Int32.Parse(reader["NumberOfQuestions"].ToString());
                        reader.Close();
                    }
                }
                catch (Exception e)
                {
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}