using ExamServer.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamServer.Mvc.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public bool RandomizeAnswers { get; set; }
        public List<Answer> Answers { get; set; }

        // Example function to shuffle the answers list if randomizeAnswers is true
        public void ShuffleAnswers()
        {
            if (RandomizeAnswers)
            {
                var random = new Random();
                Answers = Answers.OrderBy(x => random.Next()).ToList();
            }
        }

        // Example function to check if the given answer is correct
        public bool IsCorrectAnswer(Answer selectedAnswer)
        {
            var correctAnswer = Answers.FirstOrDefault(a => a.IsCorrect);
            return selectedAnswer == correctAnswer;
        }
    }
}
