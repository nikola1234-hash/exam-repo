namespace ExamServer.EntityFramework.Entities
{
    public class ExamResult : BaseObject
    {
        public Guid ExamId { get; set; }
        public Exam Exam { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int Grade { get; set; }
        public ICollection<Error> Errors { get; set; }
        public ExamResult()
        {
            Errors = new List<Error>();
        }
    }
}
