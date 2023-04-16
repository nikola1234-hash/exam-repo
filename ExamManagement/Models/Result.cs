using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTestMaker.Models
{
    public class Result
    {
        public Result(Question question, Answer answer)
        {
            Question = question;
            Answer = answer;
        }

        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }
}
