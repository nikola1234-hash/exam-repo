using Server.EntityFramework.Entities;

namespace Server.Mvc.Models
{
    public class GradingData
    {
        public StudentExam StudentExam { get; set; }
        public int StudentId { get; set; }
        public Exam Exam { get; set; }
    }
}
