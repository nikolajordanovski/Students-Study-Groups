using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace Students_Study_Groups
{
    public partial class Questions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                pullQuestions();
        }

        protected void pullQuestions()
        {
            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["SSG"].ConnectionString;

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Questions";

            SqlDataReader reader;

            try
            {
                connection.Open();
                reader = command.ExecuteReader();

                while(reader.Read())
                {
                    string title = reader[2].ToString();
                    string body = reader[3].ToString();
                    string votes = reader[4].ToString();
                    string views = reader[5].ToString();
                }
            }
            catch (Exception ex)
            {
                Label lblError = (Label)Master.FindControl("lblError");
                lblError.Text = ex.Message;
            }
            finally
            {
                connection.Close();
            }
        }

        protected void generateDivQuestion(string title, string body, string votes, string views)
        {
            string questionDiv = "";
            
            questionDiv += "<div class='question'>";
            questionDiv += "<div class='left'>";
            questionDiv += "<div>" + votes + "</div>";
            questionDiv += "<div>Votes</div>";
            questionDiv += "<div>" + views + "</div>";
            questionDiv += "<div>Answers</div>";
            questionDiv += "</div>";
            questionDiv += "<div class='right'>";
            questionDiv += "<div class='title'>";
            questionDiv += title;
            questionDiv += "</div>";
            questionDiv += "<div class='description'>";
            questionDiv += body;
            questionDiv += "</div>";
            questionDiv += "<div class='tags'>";
            questionDiv += "<div class='tag'>Java</div>";
            questionDiv += "<div class='tag'>JSON</div>";
            questionDiv += "<div class='tag'>Android</div>";
            questionDiv += "<div class='tag'>GitHub</div>";
            questionDiv += "</div>";
            questionDiv += "</div>";
            questionDiv += "</div>";
        }
    }
}