using ExamServer.EntityFramework.Entities;

namespace ExamServer.Mvc.Models
{
    public class Result
    {
        public Result(Question question, Answer answer)
        {
            Question = question;
            Answer = answer;
        }

        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }
}
