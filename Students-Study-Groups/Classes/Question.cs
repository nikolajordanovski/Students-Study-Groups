using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Students_Study_Groups.Classes
{
    public class Question
    {
        public int QID                  { get; set; }
        public int UID                  { get; set; }
        public string Title             { get; set; }
        public int Views                { get; set; }
        public int Votes                { get; set; }
        public string DateAsked         { get; set; }
        public string Body              { get; set; }
        public List<Comment> Comments   { get; set; }
        public List<Answer> Answers     { get; set; }
        
        public class Comment
        {
            public int CID              { get; set; }
            public int UID              { get; set; }
            public string Text          { get; set; }
            public string DateCommented { get; set; }
        }

        public Question(int qid, List<int> answerIds)
        {
            Comments = new List<Comment>();
            Answers  = new List<Answer>();

            foreach (int aid in answerIds)
            {
                Answer a = new Answer(aid);
                Answers.Add(a);
            }

            GetQuestionData(qid);
        }

        private void GetQuestionData(int qid) 
        {
            
        }
    }
}