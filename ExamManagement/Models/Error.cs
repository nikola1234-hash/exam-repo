using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamManagement.Models
{
    public class Error
    {
        public Error(string questionName, string selectedAnswer, string correctAnswer)
        {
            QuestionName = questionName;
            SelectedAnswer = selectedAnswer;
            CorrectAnswer = correctAnswer;
        }

        public string QuestionName { get; set; }
        public string SelectedAnswer { get; set; }
        public string CorrectAnswer { get; set; }
    }
}
