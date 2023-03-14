namespace ExamServer.EntityFramework.Entities
{
    public class ExamResult : BaseObject
    {
        public string User { get; set; }  
        public List<QuestionResult> Results { get; set; }
    }
}
