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
        private int SID, TID;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["order"] = "ORDER BY q.DateAsked DESC";
            if (Request.QueryString["SID"] != null)
                SID = Int32.Parse(Request.QueryString["SID"]);
            else
                SID = -1;
            if (Request.QueryString["TID"] != null)
                TID = Int32.Parse(Request.QueryString["TID"]);
            else
                TID = -1;
            if (!IsPostBack)
                pullQuestions();
        }

        protected void pullQuestions()
        {
            bool first = true;
            dvQuestions.InnerHtml = "";
            
            List<Classes.Question> questions = QuestionsModel.GetQuestionsData((string) Session["order"], SID, TID);
            foreach (Classes.Question question in questions)
            {
                dvQuestions.InnerHtml += generateDivQuestion(first, question.QID, question.UID, question.Username, question.Title, question.Body, question.Votes, question.Views, question.DateAsked, question.Answers, question.Tags);
                first = false;
            }
        }

        protected string generateDivQuestion(bool first, int QID, int UID, string username, string title, string body, int votes, int views, string dateAsked, int answers, List<Tag> tags)
        {
            StringBuilder questionDiv = new StringBuilder();
            string description = HtmlRemoval.StripTagsRegex(body);
            
            if(first)
                questionDiv.Append("<div class='questionfirst'>");
            else
                questionDiv.Append("<div class='question'>");
            questionDiv.Append("<div class='left'>");
            questionDiv.Append("<table><tr><td>Votes:</td><td>" + votes + "</td></tr>");
            questionDiv.Append("<tr><td>Views:</td><td>" + views + "</td></tr>");
            questionDiv.Append("<tr><td>Answers:</td><td>" + answers + "</td></tr></table>");
            questionDiv.Append("</div>");
            questionDiv.Append("<div class='right'>");
            questionDiv.Append("<div class='title'><a href='Question.aspx?QID=" + QID + "'>");
            questionDiv.Append(title);
            questionDiv.Append("</a></div>");
            questionDiv.Append("<div class='description'>");
            if(description.Length > 200)
                questionDiv.Append(description.Substring(0, 195) + "...");
            else
                questionDiv.Append(description);
            questionDiv.Append("</div>");
            questionDiv.Append("<div class='about'>");
            questionDiv.Append("<div class='tags'>");
            foreach(Tag tag in tags)
                questionDiv.Append("<div class='tag'><a href='Questions.aspx?TID=" + tag.TID + "'>" + tag.Name.Trim() + "</a></div>");
            questionDiv.Append("</div>");
            questionDiv.Append("<div class='profile'>");
            questionDiv.Append("Asked by: <a href='Profile.aspx?UID=" + UID + "'><b>" + username + "</b></a> on <b>" + DateTime.Parse(dateAsked).ToShortDateString() + "</b>");
            questionDiv.Append("</div>");
            questionDiv.Append("</div>");
            questionDiv.Append("</div>");
            questionDiv.Append("</div>");

            return questionDiv.ToString();
        }

        protected void lbNewest_Click(object sender, EventArgs e)
        {
            Session["order"] = "ORDER BY q.DateAsked DESC";
            pullQuestions();
        }

        protected void lbVotes_Click(object sender, EventArgs e)
        {
            Session["order"] = "ORDER BY q.Votes DESC";
            pullQuestions();
        }

        protected void lbViews_Click(object sender, EventArgs e)
        {
            Session["order"] = "ORDER BY q.Views DESC";
            pullQuestions();
        }

        protected void lbUnanswered_Click(object sender, EventArgs e)
        {
            Session["order"] = "ISNULL(a.Answers, 0) = 0 ORDER BY q.DateAsked DESC";
            pullQuestions();
        }
    }
}