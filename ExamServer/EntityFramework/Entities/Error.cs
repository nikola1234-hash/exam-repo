namespace ExamServer.EntityFramework.Entities
{
    public class Error : BaseObject
    {
        public Error(string questionName, string wrongAnswer, string correctAnswer)
        {
            QuestionName = questionName;
            WrongAnswer = wrongAnswer;
            CorrectAnswer = correctAnswer;
        }

        public string QuestionName { get; set; }
        public string WrongAnswer { get; set; }
        public string CorrectAnswer { get; set; }
    }
}