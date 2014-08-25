using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Students_Study_Groups.Classes
{
    public class Answer
    {
        public int AID { get; set; }
        public int QID { get; set; }
        public int UID { get; set; }
        public string Body { get; set; }
        public int Votes { get; set; }
        public int Correct { get; set; }
        public string DateAnswered { get; set; }
    }
}