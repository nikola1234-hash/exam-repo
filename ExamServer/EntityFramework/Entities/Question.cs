namespace ExamServer.EntityFramework.Entities
{
    public class Question : BaseObject
    {
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public bool RandomizeAnswers { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
