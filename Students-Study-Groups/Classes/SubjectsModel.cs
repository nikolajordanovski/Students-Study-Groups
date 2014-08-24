using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Students_Study_Groups.Classes
{
    public class SubjectsModel
    {
        public static List<Subject> GetSubjectsData()
        {
            List<Subject> subjects = new List<Subject>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["SSG"].ConnectionString;
                try
                {
                    conn.Open();
                    SqlDataReader reader;
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;

                    command.CommandText = "SELECT * FROM Subjects";
                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Subject subject = new Subject();
                            subject.SID = Int32.Parse(reader["SID"].ToString());
                            subject.Name = reader["Name"].ToString();
                            subject.Questions = Int32.Parse(reader["Questions"].ToString());
                            subject.Views = Int32.Parse(reader["Views"].ToString());
                            subject.Users = Int32.Parse(reader["Users"].ToString());
                            subject.Description = reader["Description"].ToString();
                            subjects.Add(subject);
                        }
                    }
                    reader.Close();
                    return subjects;
                }
                catch { return subjects; }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}