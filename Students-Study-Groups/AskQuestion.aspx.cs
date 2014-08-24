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
            string id = Request.QueryString["QID"];

            //Show error page if the query string is empty or not integer
            if (string.IsNullOrEmpty(id) || !Int32.TryParse(id, out SID))
            {
                Response.Redirect("Error.aspx");
            }

            if (HttpContext.Current.Session["User"] != null)
            {
                UID = Int32.Parse(HttpContext.Current.Session["UID"].ToString());
            }
        }

    }
}