using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Students_Study_Groups.Classes
{
    public class QuestionsModel
    {
        public static List<Question> GetQuestionsData(string orderBy)
        {
            List<Question> questions = new List<Question>();

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["SSG"].ConnectionString;
                try
                {
                    conn.Open();
                    SqlDataReader reader;
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;

                    command.CommandText = "SELECT q.QID, q.UID, u.Username, q.Title, q.Body, q.Votes, q.Views, q.DateAsked, ISNULL(a.Answers, 0) As 'Answers',  t.TID, t.Name AS 'Tag' " +
                                          "FROM Questions q " +
                                          "INNER JOIN Users u ON q.UID = u.UID " +
                                          "LEFT JOIN (SELECT QID, COUNT(AID) As Answers FROM Answers GROUP BY QID) a ON q.QID = a.QID " + 
                                          "INNER JOIN QHasT qt ON q.QID = qt.QID " +
                                          "INNER JOIN Tags t ON t.TID = qt.TID " +
                                          orderBy;

                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Question question = null;

                        while (reader.Read())
                        {
                            if (question == null)
                            {
                                question = new Question();

                                question.QID = Int32.Parse(reader["QID"].ToString());
                                question.UID = Int32.Parse(reader["UID"].ToString());
                                question.Username = reader["Username"].ToString();
                                question.Title = reader["Title"].ToString();
                                question.Body = reader["Body"].ToString();
                                question.Votes = Int32.Parse(reader["Votes"].ToString());
                                question.Views = Int32.Parse(reader["Views"].ToString());
                                question.DateAsked = reader["DateAsked"].ToString();
                                question.Answers = Int32.Parse(reader["Answers"].ToString());
                                question.Tags = new List<Tag>();
                                Tag tag = new Tag();
                                tag.TID = Int32.Parse(reader["TID"].ToString());
                                tag.Name = reader["Tag"].ToString();
                                question.Tags.Add(tag);
                            }
                            else if (question.QID == Int32.Parse(reader["QID"].ToString()))
                            {
                                Tag tag = new Tag();
                                tag.TID = Int32.Parse(reader["TID"].ToString());
                                tag.Name = reader["Tag"].ToString();
                                question.Tags.Add(tag);
                            }
                            else 
                            {
                                questions.Add(question);
                                question = new Question();

                                question.QID = Int32.Parse(reader["QID"].ToString());
                                question.UID = Int32.Parse(reader["UID"].ToString());
                                question.Username = reader["Username"].ToString();
                                question.Title = reader["Title"].ToString();
                                question.Body = reader["Body"].ToString();
                                question.Votes = Int32.Parse(reader["Votes"].ToString());
                                question.Views = Int32.Parse(reader["Views"].ToString());
                                question.DateAsked = reader["DateAsked"].ToString();
                                question.Answers = Int32.Parse(reader["Answers"].ToString());
                                question.Tags = new List<Tag>();
                                Tag tag = new Tag();
                                tag.TID = Int32.Parse(reader["TID"].ToString());
                                tag.Name = reader["Tag"].ToString();
                                question.Tags.Add(tag);
                            }
                        }
                        questions.Add(question);
                    }
                    reader.Close();
                    return questions;
                }
                catch { return questions; }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}