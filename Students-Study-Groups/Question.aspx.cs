using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace Students_Study_Groups
{
    public partial class Question : System.Web.UI.Page
    {
        public int QID;
        public Question Question;

        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["QID"];

            if (string.IsNullOrEmpty(id) || !Int32.TryParse(id, out QID))
            {
                Response.Redirect("Error.aspx");
            }
            else if(!IsPostBack)
            {

            }
        }

        public void FillQuestion()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["SSG"].ConnectionString;
                try
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand();
                    SqlDataReader reader;
                    command.Connection = conn;


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