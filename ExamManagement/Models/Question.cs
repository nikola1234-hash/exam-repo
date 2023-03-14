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
                if(value == null)
                {
                    return;
                }
                var random = new Random();
                value = value.OrderBy(x => random.Next()).ToList();
                SetField(ref _answers, value, nameof(Answers));
            }
        }

        // Example function to shuffle the answers list if randomizeAnswers is true
     

   
    }
}
