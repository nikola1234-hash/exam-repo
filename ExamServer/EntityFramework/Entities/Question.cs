namespace ExamServer.EntityFramework.Entities
{
    public class Question : BaseObject
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public bool IsImageQuestion { get; set; }
        public bool RandomSorting { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}
