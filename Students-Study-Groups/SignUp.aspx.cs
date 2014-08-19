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
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lbSignUp_Click(object sender, EventArgs e)
        {
            string Name = tbName.Text;
            string Email = tbEmail.Text;
            string Surname = tbSurname.Text;
            string Username = tbUsername.Text;
            string Password = tbPassword.Text;

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["SSG"].ConnectionString;
                try
                {
                    conn.Open();
                    
                    string query = "SELECT * FROM Users WHERE Username = @Username OR Email = @Email";
                    
                    SqlCommand command = new SqlCommand(query, conn);
                    command.Parameters.AddWithValue("@Username", Username);
                    command.Parameters.AddWithValue("@Email", Email);
                    SqlDataReader reader = command.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        reader.Close();
                        query = "INSERT INTO Users (Name, Surname, Username, Email, Password, Reputation) VALUES (@Name, @Surname, @Username, @Email, @Password, 0)";
                        command.CommandText = query;
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@Surname", Surname);
                        command.Parameters.AddWithValue("@Username", Username);
                        command.Parameters.AddWithValue("@Email", Email);
                        command.Parameters.AddWithValue("@Password", Password);
                        command.ExecuteNonQuery();

                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:popup(); ", true);
                    }
                    else 
                    {
                        lblError.Text = "The username or email already exists. Please use another.";
                        lblError.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    lblError.Text = ex.Message;
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