using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Students_Study_Groups.Classes
{
    public class Question
    {
        public int QID { get; set; }
        public int UID { get; set; }
        public string Username { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int Votes { get; set; }
        public int Views { get; set; }
        public string DateAsked { get; set; }
        public int Answers { get; set; }
        public List<Tag> Tags { get; set; }
    }
}