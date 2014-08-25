using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace Students_Study_Groups.Classes
{
    public class AnswerModel
    {
        public class Comment
        {
            public int CID              { get; set; }
            public int UID              { get; set; }
            public string Username      { get; set; }
            public string Text          { get; set; }
            public string DateCommented { get; set; }
        }

        public int AID                          { get; set; }
        public int UID                          { get; set; }
        public string Username                  { get; set; }
        public int Correct                      { get; set; }
        public int Votes                        { get; set; }
        public string DateAnswered              { get; set; }
        public string Body                      { get; set; }
        public List<Comment> Comments           { get; set; }
        public Dictionary<int, int> UsersVoted  { get; set; }
        
        public AnswerModel(int id)
        {
            Comments   = new List<Comment>();
            UsersVoted = new Dictionary<int, int>();
            GetAnswerData(id);
        }

        private void GetAnswerData(int id) 
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

                    command.CommandText = "SELECT * FROM Answers INNER JOIN Users ON Answers.UID = Users.UID WHERE Answers.AID = @AID";
                    command.Parameters.AddWithValue("@AID", id);
                    reader = command.ExecuteReader();
                    
                    if (reader.HasRows)
                    {
                        while(reader.Read()) 
                        {
                            AID          = Int32.Parse(reader["AID"].ToString());
                            UID          = Int32.Parse(reader["UID"].ToString());
                            Correct      = Int32.Parse(reader["Correct"].ToString());
                            Votes        = Int32.Parse(reader["Votes"].ToString());
                            Body         = reader["Body"].ToString();
                            DateAnswered = reader["DateAnswered"].ToString();
                            Username     = reader["Username"].ToString();
                        }
                    }
                    reader.Close();

                    command.CommandText = "SELECT ac.*, u.Username FROM AnswerComments as ac, Users as u WHERE ac.AID = @AID AND ac.UID = u.UID";
                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Comment c       = new Comment();
                            c.CID           = Int32.Parse(reader["CID"].ToString());
                            c.UID           = Int32.Parse(reader["UID"].ToString());
                            c.Text          = reader["Text"].ToString();
                            c.DateCommented = reader["DateCommented"].ToString();
                            c.Username      = reader["Username"].ToString();
                            Comments.Add(c);
                        }
                    }
                    reader.Close();

                    command.CommandText = "SELECT * FROM UVoteA WHERE AID = @AID";
                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            UsersVoted.Add(Int32.Parse(reader["UID"].ToString()), Int32.Parse(reader["Vote"].ToString()));
                        }
                    }
                    reader.Close();
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
