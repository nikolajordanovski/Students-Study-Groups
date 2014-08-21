using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using Students_Study_Groups.Classes;

namespace Students_Study_Groups
{
    public partial class Questions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["order"] = "ORDER BY DateAsked DESC";
                pullQuestions();
            }
        }

        protected void pullQuestions()
        {
            questions.InnerHtml = "";

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["SSG"].ConnectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT QID FROM Questions " + Session["order"];
            SqlDataReader reader;

            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                    questions.InnerHtml += generateDivQuestion(reader.GetInt32(0));
            }
            catch {}
            finally
            {
                connection.Close();
            }
        }

        protected string generateDivQuestion(int qid)
        {
            QuestionModel question = QuestionModel.GetQuestionData(qid);

            StringBuilder questionDiv = new StringBuilder();
            string description = HtmlRemoval.StripTagsRegex(question.Body);

            questionDiv.Append("<div class='question'>");
            questionDiv.Append("<div class='left'>");
            questionDiv.Append("<table><tr><td>Votes:</td><td>" + question.Votes + "</td></tr>");
            questionDiv.Append("<tr><td>Views:</td><td>" + question.Views + "</td></tr>");
            questionDiv.Append("<tr><td>Answers:</td><td>" + question.Answers.Count + "</td></tr></table>");
            questionDiv.Append("</div>");
            questionDiv.Append("<div class='right'>");
            questionDiv.Append("<div class='title'><a href='Question.aspx?QID=" + qid + "'>");
            questionDiv.Append(question.Title);
            questionDiv.Append("</a></div>");
            questionDiv.Append("<div class='description'>");
            if(description.Length > 200)
                questionDiv.Append(description.Substring(0, 195) + "...");
            else
                questionDiv.Append(description);
            questionDiv.Append("</div>");
            questionDiv.Append("<div class='about'>");
            questionDiv.Append("<div class='tags'>");
            foreach(TagsModel tag in question.Tags)
            questionDiv.Append("<div class='tag'>" + tag.Name.Trim() + "</div>");
            questionDiv.Append("</div>");
            questionDiv.Append("<div class='profile'>");
            questionDiv.Append("Asked by: <a href='Profile.aspx?UID=" + question.UID + "'><b>" + question.Username + "</b></a> on <b>" + DateTime.Parse(question.DateAsked).ToShortDateString() + "</b>");
            questionDiv.Append("</div>");
            questionDiv.Append("</div>");
            questionDiv.Append("</div>");
            questionDiv.Append("</div>");

            return questionDiv.ToString();
        }

        protected void lbNewest_Click(object sender, EventArgs e)
        {
            Session["order"] = "ORDER BY DateAsked DESC";
            pullQuestions();
        }

        protected void lbVotes_Click(object sender, EventArgs e)
        {
            Session["order"] = "ORDER BY Votes DESC";
            pullQuestions();
        }

        protected void lbViews_Click(object sender, EventArgs e)
        {
            Session["order"] = "ORDER BY Views DESC";
            pullQuestions();
        }

        protected void lbUnanswered_Click(object sender, EventArgs e)
        {
            Session["order"] = "WHERE QID NOT IN (SELECT QID FROM Answers) ORDER BY Questions.DateAsked DESC";
            pullQuestions();
        }
    }
}