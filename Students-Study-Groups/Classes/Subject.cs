using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Students_Study_Groups.Classes
{
    public class Subject
    {
        public int SID { get; set; }
        public string Name { get; set; }
        public int Questions { get; set; }
        public int Views { get; set; }
        public int Users { get; set; }
        public string Description { get; set; }
    }
}