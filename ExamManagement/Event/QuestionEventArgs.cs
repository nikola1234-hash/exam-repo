using Prism.Events;
using System;

namespace ExamManagement.Event
{
    public class QuestionEventArgs 
    { 
        public object Arg;

        public QuestionEventArgs(object arg)
        {
            Arg = arg;
        }
    }
}