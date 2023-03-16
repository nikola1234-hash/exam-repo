using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamManagement.Models
{
    public class Error
    {
        public Error(string questionName, string wrongAnswer, string correctAnswer)
        {
            QuestionName = questionName;
            WrongAnswer = wrongAnswer;
            CorrectAnswer = correctAnswer;
        }

        public string QuestionName { get; set; }
        public string WrongAnswer { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
