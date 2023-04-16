using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTestMaker.Models
{
    public class StudentExam
    {
        public string StudentName { get; set; }
        public List<int> SelectedAnswers { get; set; }
        public StudentExam()
        {
            SelectedAnswers = new List<int>();
        }

       
    }
}
