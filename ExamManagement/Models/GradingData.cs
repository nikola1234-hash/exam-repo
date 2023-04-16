using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTestMaker.Models
{
    public class GradingData
    {
        public GradingData(StudentExam studentExam, int studentId, Exam exam)
        {
            StudentExam = studentExam;
            StudentId = studentId;
            Exam = exam;
        }

        public StudentExam StudentExam { get; set; }
        public int StudentId { get; set; }
        public Exam Exam { get; set; }
    }
}
