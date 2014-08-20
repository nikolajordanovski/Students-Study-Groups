using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using Students_Study_Groups.Classes;

namespace Students_Study_Groups
{
    public partial class Question : System.Web.UI.Page
    {
        public int QID;
        public QuestionModel QuestionMod;

        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["QID"];

            if (string.IsNullOrEmpty(id) || !Int32.TryParse(id, out QID))
            {
                Response.Redirect("Error.aspx");
            }
            else if(!IsPostBack)
            {
                QuestionMod = QuestionModel.GetQuestionData(QID);
                if (QuestionMod == null)
                {
                    Response.Redirect("Error.aspx");
                }
            }
        }

    }
}