using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamManagement.Models
{
    public class ExamResult
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public Exam Exam { get; set; }
        public Guid ExamId { get; set; }
        public int Grade { get; set; }
        public List<Error> Errors { get; set; }
        public ExamResult()
        {
            Errors= new List<Error>();
        }

        public ExamResult(int studentId, string studentName, Guid examId, int grade, List<Error> errors)
        {
            StudentId = studentId;
            StudentName = studentName;
            ExamId = examId;
            Grade = grade;
            Errors = errors;
        }
    }
}