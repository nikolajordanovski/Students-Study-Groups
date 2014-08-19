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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["User"] != null)
            {
                UID = Int32.Parse(HttpContext.Current.Session["UID"].ToString());
            }
        }

    }
}