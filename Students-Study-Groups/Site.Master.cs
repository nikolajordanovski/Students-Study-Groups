using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Students_Study_Groups
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        public int UID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["User"] != null)
            {
                login.InnerText = "Logout";
                login.HRef = "javascript:logout();";
            }
        }

        protected void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string title = tbSearch.Text;
            Response.Redirect("Questions.aspx?Title=" + title);
        }
    }
}
