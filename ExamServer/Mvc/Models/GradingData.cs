using ExamServer.EntityFramework.Entities;

namespace ExamServer.Mvc.Models
{
    public class GradingData
    {
        public StudentExam StudentExam { get; set; }
        public int StudentId { get; set; }
        public Exam Exam { get; set; }
    }
}
