using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Students_Study_Groups.Classes;
using System.Text;

namespace Students_Study_Groups
{
    public partial class Index : System.Web.UI.Page
    {
        public List<Subject> subjects;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                generateSubjectsTable();
        }

        protected void generateSubjectsTable()
        {
            subjects = SubjectsModel.GetSubjectsData();
            int columnsCount = 0;
            TableRow row = new TableRow();

            foreach (Subject subject in subjects)
            {
                if (columnsCount == 6)
                {
                    tbSubjects.Rows.Add(row);
                    row = new TableRow();
                    columnsCount = 0;
                }

                TableCell cell = new TableCell();
                cell.Text = generateSubjectDiv(subject.SID, subject.Name, subject.Views, subject.Questions);
                row.Cells.Add(cell);
                columnsCount++;
            }
        }

        protected string generateSubjectDiv(int SID, string name, int views, int questions)
        {
            StringBuilder subject = new StringBuilder();

            subject.Append("<div class='subject' onclick='javascript:subjectClicked(" + SID + "); return true;'>");
            if (name.Length > 30)
                subject.Append("<div class='name'>" + name.Substring(0, 30) + "...</div>");
            else
                subject.Append("<div class='name'>" + name + "</div>");
            subject.Append("<div class='views'>" + views + " views</div>");
            subject.Append("<div class='questions'>" + questions + " questions</div>");
            subject.Append("</div>");

            return subject.ToString();
        }
    }
}