using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamManagement.Models
{
    public class Error
    {
        public Error(string questionName, string wrongAnswerSelected, string correctAnswer)
        {
            QuestionName = questionName;
            WrongAnswerSelected = wrongAnswerSelected;
            CorrectAnswer = correctAnswer;
        }

        public string QuestionName { get; set; }
            public string WrongAnswerSelected { get; set; }
            public string CorrectAnswer { get; set; }
    }
}
