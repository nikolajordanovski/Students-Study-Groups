using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using Students_Study_Groups.Classes;
using System.Text;

namespace Students_Study_Groups
{
    public partial class Question : System.Web.UI.Page
    {
        public int QID;
        public int UID;
        public QuestionModel QuestionMod;

        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["QID"];

            //Show error page if the query string is empty or not integer
            if (string.IsNullOrEmpty(id) || !Int32.TryParse(id, out QID))
            {
                Response.Redirect("Error.aspx");
            }

            if (HttpContext.Current.Session["User"] != null)
            {
                UID = Int32.Parse(HttpContext.Current.Session["UID"].ToString());
            }
            
            if(!IsPostBack)
            { 
                QuestionMod = QuestionModel.GetQuestionData(QID);
                if (QuestionMod == null)
                {
                    Response.Redirect("Error.aspx");
                }

                FillQuestionHtml();

                if (QuestionMod.Answers.Count > 0) 
                    FillAnswersHtml();
            }
        }

        private void FillQuestionHtml()
        {
            StringBuilder innerHtml = new StringBuilder();

            innerHtml.Append("<div class='body-content'>");
            innerHtml.Append(QuestionMod.Body);
            innerHtml.Append("  <div>");
            innerHtml.Append("      <div class='question-tags'>");
            foreach (TagModel Tag in QuestionMod.Tags)
            {
                innerHtml.Append("      <div class='tag'><a href='#'>" + Tag.Name + "</a></div>");
            }
            innerHtml.Append("      </div>");
            innerHtml.Append("      <div style='float:right;'>Asked by: <a href='Profile.aspx?UID="+ QuestionMod.UID +"'>" + QuestionMod.Username + "</a></div>");
            innerHtml.Append("  </div>");
            innerHtml.Append("</div>");
            innerHtml.Append("<div class='question-comments'>");
            innerHtml.Append("  <a href='javascript:void(0)' id='add_question_comment' class='button-comment float-left' style='margin-right: 10px;'>add comment</a>");
            innerHtml.Append("  <div class='question-post-comment' style='display:none;'>");
            innerHtml.Append("      <textarea id='ta_question_comment' class='textarea-input'></textarea>");
            innerHtml.Append("      <a href='javascript:void(0)' id='post_question_comment' class='button-comment float-right' style='margin-right: 10px; width: 30px !important;'>post</a>");
            innerHtml.Append("  </div>");
            
            //Question comments
            if (QuestionMod.Comments.Count > 0)
            {
                innerHtml.Append("<div class='question-comments-wrap'>");
                innerHtml.Append("  <span>Comments:</span><hr />");
                foreach (Students_Study_Groups.Classes.QuestionModel.Comment c in QuestionMod.Comments)
                {
                    innerHtml.Append("<div class='comment'>");
                    innerHtml.Append("  <span>" + c.Text + "</span>");
                    innerHtml.Append("  <span>--</span><a href='Profile.aspx?UID="+ c.UID +"'>" + c.Username + "</a>");
                    innerHtml.Append("</div>");
                }
                innerHtml.Append("</div>");
            }
            innerHtml.Append("</div>");

            QuestionBody.InnerHtml = innerHtml.ToString();
        }

        private void FillAnswersHtml()
        {
            StringBuilder innerHtml = new StringBuilder();

            innerHtml.Append("<div class='answers-header'>");
            innerHtml.Append("  <h1>Answers</h1>");
            innerHtml.Append("</div>");

            foreach(AnswerModel answer in QuestionMod.Answers) 
            {
                innerHtml.Append("<div class='answer-content'>");
                innerHtml.Append("  <div class='answer-votes'></div>");
                innerHtml.Append("  <div class='answer-body'>");
                innerHtml.Append("      <div class='body-content'>");
                innerHtml.Append(answer.Body);
                innerHtml.Append("          <div style='float:right;display:inline;'>Posted by: <a href='Profile.aspx?UID="+ answer.UID +"'>" + answer.Username + "</a></div>");
                innerHtml.Append("      </div>");
                innerHtml.Append("      <div class='answer-comments'>");
                innerHtml.Append("          <a href='javascript:void(0)' id='add_answer_comment_"+ answer.AID + "' class='button-comment float-left' style='margin-right: 10px;'>add comment</a>");
                innerHtml.Append("          <div id='answer_post_comment_" + answer.AID + "' class='question-post-comment' style='display:none;'>");
                innerHtml.Append("              <textarea id='ta_answer_comment_"+ answer.AID + "' class='textarea-input'></textarea>");
                innerHtml.Append("              <a href='javascript:void(0)' id='post_answer_comment_" + answer.AID +"' class='button-comment float-right' style='margin-right: 10px; width: 30px !important;'>post</a>");
                innerHtml.Append("          </div>");
                //Answer comments
                if (answer.Comments.Count > 0)
                {
                    innerHtml.Append("      <div class='answer-comments-wrap'>");
                    innerHtml.Append("          <span>Comments:</span><hr />");
                    foreach (Students_Study_Groups.Classes.AnswerModel.Comment c in answer.Comments)
                    {
                        innerHtml.Append("      <div class='comment'>");
                        innerHtml.Append("          <span>" + c.Text + "</span>");
                        innerHtml.Append("          <span>--</span><a href='Profile.aspx?UID="+ c.UID +"'>" + c.Username + "</a>");
                        innerHtml.Append("      </div>");
                    }
                    innerHtml.Append("      </div>");
                }
                innerHtml.Append("      </div>");
                innerHtml.Append("  </div>");
                innerHtml.Append("</div>");
            }

            
            AnswersContent.InnerHtml = innerHtml.ToString();
        }

    }
}