using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace Students_Study_Groups.Classes
{
    public class QuestionModel
    {
        public class Comment
        {
            public int CID              { get; set; }
            public int UID              { get; set; }
            public string Username      { get; set; }
            public string Text          { get; set; }
            public string DateCommented { get; set; }
        }

        public int QID                   { get; set; }
        public int UID                   { get; set; }
        public string Username           { get; set; }
        public string Title              { get; set; }
        public int Views                 { get; set; }
        public int Votes                 { get; set; }
        public string DateAsked          { get; set; }
        public string Body               { get; set; }
        public List<Comment> Comments    { get; set; }
        public List<AnswerModel> Answers { get; set; }
        public List<TagModel> Tags      { get; set; }

        public static QuestionModel GetQuestionData(int qid) 
        {
            QuestionModel question = null;
            
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["SSG"].ConnectionString;
                try
                {
                    conn.Open();
                    SqlDataReader reader;
                    SqlCommand command = new SqlCommand();
                    command.Connection = conn;
                    command.CommandText = "SELECT * FROM Questions INNER JOIN Users ON Questions.UID = Users.UID WHERE QID = @QID";
                    command.Parameters.AddWithValue("@QID", qid);
                    reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        question = new QuestionModel();
                        question.Comments   = new List<Comment>();
                        question.Answers    = new List<AnswerModel>();
                        question.Tags       = new List<TagModel>();
                        
                        //Take the question data
                        reader.Read();
                        question.QID        = Int32.Parse(reader["QID"].ToString());
                        question.UID        = Int32.Parse(reader["UID"].ToString());
                        question.Username   = reader["username"].ToString();
                        question.Votes      = Int32.Parse(reader["Votes"].ToString());
                        question.Views      = Int32.Parse(reader["Views"].ToString());
                        question.Title      = reader["Title"].ToString();
                        question.Body       = reader["Body"].ToString();
                        question.DateAsked  = reader["DateAsked"].ToString();
                        reader.Close();

                        //Take all the question comments
                        command.CommandText = "SELECT qc.*, u.Username FROM QuestionComments as qc, Users as u WHERE qc.QID = @QID AND qc.UID = u.UID";
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
                                question.Comments.Add(c);
                            }
                        }
                        reader.Close();

                        //Take all the answers for the question (and comments for the answers)
                        command.CommandText = "SELECT AID FROM Answers WHERE QID = @QID";
                        reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                AnswerModel ans = new AnswerModel(Int32.Parse(reader["AID"].ToString()));
                                question.Answers.Add(ans);
                            }
                        }
                        reader.Close();

                        //Take the tags for the question
                        command.CommandText = "SELECT TID FROM QHasT WHERE QID = @QID";
                        reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                TagModel tag = new TagModel(Int32.Parse(reader["TID"].ToString()));
                                question.Tags.Add(tag);
                            }
                        }
                        reader.Close();

                        return question;
                    }
                    else
                    {
                        return null;
                    }

                }
                catch (Exception e)
                {
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