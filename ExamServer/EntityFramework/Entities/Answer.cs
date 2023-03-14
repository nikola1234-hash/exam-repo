namespace ExamServer.EntityFramework.Entities
{
    public class Answer : BaseObject
    {
       
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
