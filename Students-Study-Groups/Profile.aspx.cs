using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Students_Study_Groups.Classes;
using System.Text;

namespace Students_Study_Groups
{
    public partial class Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                pullUserData(Int32.Parse(Request.QueryString["UID"]));
        }

        protected void pullUserData(int UID)
        {
            User user = UserModel.getUserData(UID);
            username.InnerHtml = user.Username;
            fillUserInfo(user);
            questionsNumber.InnerHtml = user.questions.Count + " Questions";
            fillQuestions(user.questions);
            answersNumber.InnerHtml = user.answers.Count + " Answers";
            fillAnswers(user.answers);
        }

        protected void fillUserInfo(User user)
        {
            TableRow row = new TableRow();
            TableCell cell1 = new TableCell();
            cell1.Text = "Name: ";
            row.Cells.Add(cell1);
            TableCell cell2 = new TableCell();
            cell2.Text = user.Name;
            row.Cells.Add(cell2);
            tblInfo.Rows.Add(row);

            row = new TableRow();
            TableCell cell3 = new TableCell();
            cell3.Text = "Surname: ";
            row.Cells.Add(cell3);
            TableCell cell4 = new TableCell();
            cell4.Text = user.Surname;
            row.Cells.Add(cell4);
            tblInfo.Rows.Add(row);

            row = new TableRow();
            TableCell cell5 = new TableCell();
            cell5.Text = "Email: ";
            row.Cells.Add(cell5);
            TableCell cell6 = new TableCell();
            cell6.Text = user.Email;
            row.Cells.Add(cell6);
            tblInfo.Rows.Add(row);

            row = new TableRow();
            TableCell cell7 = new TableCell();
            cell7.Text = "Reputation: ";
            row.Cells.Add(cell7);
            TableCell cell8 = new TableCell();
            cell8.Text = user.Reputation.ToString();
            row.Cells.Add(cell8);
            tblInfo.Rows.Add(row);
        }

        protected void fillQuestions(List<Classes.Question> questionsList)
        {
            StringBuilder questionsDiv = new StringBuilder();
            foreach(Classes.Question question in questionsList)
            {
                questionsDiv.Append("<div class='question'>");
                questionsDiv.Append("<div class='votes'>" + question.Votes + "</div>");
                if(question.Title.Length > 78)
                    questionsDiv.Append("<div class='title'><a href='Question.aspx?QID=" + question.QID + "'>" + question.Title.Substring(0, 78) + "...</a></div>");
                else
                    questionsDiv.Append("<div class='title'><a href='Question.aspx?QID=" + question.QID + "'>" + question.Title + "...</a></div>");
                questionsDiv.Append("<div class='date'>Asked: " + DateTime.Parse(question.DateAsked).ToShortDateString() + "</div>");
                questionsDiv.Append("</div>");
            }

            questions.InnerHtml = questionsDiv.ToString();
        }

        protected void fillAnswers(List<Classes.Answer> answerList)
        {
            StringBuilder answersDiv = new StringBuilder();
            foreach (Classes.Answer question in answerList)
            {
                string body = HtmlRemoval.StripTagsRegex(question.Body);

                answersDiv.Append("<div class='question'>");
                answersDiv.Append("<div class='votes'>" + question.Votes + "</div>");
                if (body.Length > 78)
                    answersDiv.Append("<div class='title'><a href='Question.aspx?QID=" + question.QID + "'>" +  body.Substring(0, 78) + "...</a></div>");
                else
                    answersDiv.Append("<div class='title'><a href='Question.aspx?QID=" + question.QID + "'>" + body + "...</a></div>");
                answersDiv.Append("<div class='date'>Answered: " + DateTime.Parse(question.DateAnswered).ToShortDateString() + "</div>");
                answersDiv.Append("</div>");
            }

            answers.InnerHtml = answersDiv.ToString();
        }
    }
}