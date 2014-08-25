using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Students_Study_Groups.Classes
{
    public class UserModel
    {
        public static User getUserData(int UID)
        {
            User user = new User();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["SSG"].ConnectionString;
                try
                {
                    conn.Open();
                    SqlDataReader reader;
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandText = "SELECT * FROM Users WHERE UID = @UID";
                    command.Parameters.AddWithValue("@UID", UID);
                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();
                        user.UID = Int32.Parse(reader["UID"].ToString());
                        user.Name = reader["Name"].ToString();
                        user.Surname = reader["Surname"].ToString();
                        user.Username = reader["Username"].ToString();
                        user.Email = reader["Email"].ToString();
                        user.Reputation = Int32.Parse(reader["Reputation"].ToString());
                    }
                    reader.Close();

                    command.CommandText = "SELECT * FROM Questions WHERE UID = @UID";
                    reader = command.ExecuteReader();

                    user.questions = new List<Question>();
                    while (reader.Read())
                    {
                        Question question = new Question();
                        question.QID = Int32.Parse(reader["QID"].ToString());
                        question.Title = reader["Title"].ToString();
                        question.Votes = Int32.Parse(reader["Votes"].ToString());
                        question.Views = Int32.Parse(reader["Views"].ToString());
                        question.DateAsked = reader["DateAsked"].ToString();
                        user.questions.Add(question);
                    }
                    reader.Close();

                    command.CommandText = "SELECT * FROM Answers WHERE UID = @UID";
                    reader = command.ExecuteReader();

                    user.answers = new List<Answer>();
                    while (reader.Read())
                    {
                        Answer answer = new Answer();
                        answer.AID = Int32.Parse(reader["AID"].ToString());
                        answer.QID = Int32.Parse(reader["QID"].ToString());
                        answer.Body = reader["Body"].ToString();
                        answer.Votes = Int32.Parse(reader["Votes"].ToString());
                        answer.Correct = Int32.Parse(reader["Correct"].ToString());
                        answer.DateAnswered = reader["DateAnswered"].ToString();
                        user.answers.Add(answer);
                    }
                    reader.Close();

                    return user;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e.Message);
                    return null;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}