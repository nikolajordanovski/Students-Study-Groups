using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Students_Study_Groups.Classes
{
    public class User
    {
        public int UID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public int Reputation { get; set; }
        public List<Question> questions { get; set; }
        public List<Answer> answers { get; set; }
    }
}