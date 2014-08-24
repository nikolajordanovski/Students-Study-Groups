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
    public partial class AskQuestion : System.Web.UI.Page
    {
        public int UID;
        public int SID;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["SID"];

            //Show error page if the query string is empty or not integer
            if (string.IsNullOrEmpty(id) || !Int32.TryParse(id, out SID))
            {
                Response.Redirect("Error.aspx");
            }

            if (HttpContext.Current.Session["User"] != null)
            {
                UID = Int32.Parse(HttpContext.Current.Session["UID"].ToString());
            }

            if (!IsPostBack)
            {
                FillTagsList();
            }
        }

        private void FillTagsList()
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

                    command.CommandText = "SELECT * FROM Tags";
                    reader = command.ExecuteReader();

                    reader.Read();
                    hfTags.Value += reader["Name"].ToString();
                    while (reader.Read())
                    {
                        hfTags.Value += "," + reader["Name"].ToString();
                    }
                    reader.Close();
                }
                catch (Exception e)
                {
                    lblError.Text = e.Message;
                    lblError.Visible = true;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}