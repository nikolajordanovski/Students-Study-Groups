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
    public partial class AskQuestion : System.Web.UI.Page
    {
        public int UID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["User"] != null)
            {
                UID = Int32.Parse(HttpContext.Current.Session["UID"].ToString());
            }

            if (!IsPostBack)
            {
                FillTagsList();
                FillSubjectsList();
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

        private void FillSubjectsList()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["SSG"].ConnectionString;
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    SqlCommand command = new SqlCommand();
                    DataSet ds = new DataSet();

                    command.Connection = conn;
                    command.CommandText = "SELECT * FROM Subjects";

                    adapter.SelectCommand = command;
                    adapter.Fill(ds, "Subjects");

                    ddlSubjects.DataSource = ds.Tables["Subjects"];
                    ddlSubjects.DataTextField = "Name";
                    ddlSubjects.DataValueField = "SID";
                    ddlSubjects.DataBind();
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