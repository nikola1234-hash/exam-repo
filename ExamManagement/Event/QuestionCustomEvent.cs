﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamManagement.Event
{
    public class QuestionCustomEvent
    {
        public object Arg;

        public QuestionCustomEvent(object arg)
        {
            Arg = arg;
        }
    }
}