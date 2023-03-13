using ExamServer.Mvc.Models;

namespace ExamServer.EntityFramework.Entities
{
    public class Exam : BaseObject
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string LecturerName { get; set; }
        public TimeSpan StartingHour { get; set; }
        public int TotalTime { get; set; }
        public bool RandomizeQuestions { get; set; }
        public ICollection<Question> Questions { get; set; }

    }
}
