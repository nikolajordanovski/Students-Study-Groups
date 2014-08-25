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

                    command.CommandText = "SELECT s.SID, s.Name, s.Questions, ISNULL(v.Views, 0) as 'Views', s.Users, s.Description " +
                                          "FROM Subjects s " +
                                          "LEFT JOIN (SELECT sq.SID, SUM(q.Views) as 'Views' FROM SHasQ sq " +
                                          "INNER JOIN Questions q ON sq.QID = q.QID GROUP BY sq.SID) v ON s.SID = v.SID";

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

        public static List<Subject> GetPopularSubjectsData()
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

                    command.CommandText = "SELECT TOP 10 * FROM Subjects ORDER BY Questions DESC";

                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Subject subject = new Subject();
                            subject.SID = Int32.Parse(reader["SID"].ToString());
                            subject.Name = reader["Name"].ToString();
                            subject.Questions = Int32.Parse(reader["Questions"].ToString());
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