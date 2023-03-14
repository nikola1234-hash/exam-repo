namespace ExamServer.EntityFramework.Entities
{
    public class Error : BaseObject
    {

        public string QuestionName { get; set; }
        public string WrongAnswer { get; set; }
        public string CorrectAnswer { get; set; }
        public int ExamResultId { get; set; }
        public ExamResult ExamResult { get; set; }
    }
}