﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamManagement.Models
{
    public class Question : BaseObject
    {
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public List<Answer> Answers { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public bool AnswersSortedRandomly { get; set; }

    }
}
