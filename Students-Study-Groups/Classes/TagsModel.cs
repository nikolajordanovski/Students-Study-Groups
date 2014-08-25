using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Students_Study_Groups.Classes
{
    public class TagsModel
    {
        public static List<Tag> GetTagsData()
        {
            List<Tag> tags = new List<Tag>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["SSG"].ConnectionString;
                try
                {
                    conn.Open();
                    SqlDataReader reader;
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;

                    command.CommandText = "SELECT TOP 10 * FROM Tags ORDER BY NumberOfQuestions DESC";
                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Tag tag = new Tag();
                            tag.TID = Int32.Parse(reader["TID"].ToString());
                            tag.Name = reader["Name"].ToString();
                            tag.NumberOfQuestions = Int32.Parse(reader["NumberOfQuestions"].ToString());
                            tags.Add(tag);
                        }
                    }
                    reader.Close();
                    return tags;
                }
                catch { return tags; }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}