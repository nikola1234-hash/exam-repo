namespace Server.EntityFramework.Entities
{
    public class Error : BaseObject
    {
        public string QuestionName { get; set; }
        public string WrongAnswerSelected { get; set; }
        public string CorrectAnswer { get; set; }  
    }
}