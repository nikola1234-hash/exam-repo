using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamManagement.Models
{
    public class Question : BaseObject
    {

        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                SetField(ref _text, value, nameof(Text));
                
            }
        }

        private string _imageUrl;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set
           { 
                SetField(ref _imageUrl, value, nameof(ImageUrl));
                
            }
        }

        private bool _randomizeAnswers;

        public bool RandomizeAnswers
        {
            get { return _randomizeAnswers; }
            set 
                
                { 
                SetField(ref _randomizeAnswers, value, nameof(RandomizeAnswers));
                }
        }
   

        private List<Answer> _answers;

        public List<Answer> Answers
        {
            get { return _answers; }
            set
            {
                SetField(ref _answers, value, nameof(Answers));
            }
        }

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
