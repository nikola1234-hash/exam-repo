using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamManagement.Models
{
    public class GradeEntity
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public Exam Exam { get; set; }
        public int NumberOfQuestions { get; set; }
        public List<Error> Errors { get; set; }
        public string Grade { get; set; }  
        public GradeEntity()
        {
            Errors = new List<Error>();
            Grade = string.Empty;
        }
    }
}
