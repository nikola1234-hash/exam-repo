using ExamServer.EntityFramework.Entities;

namespace ExamServer.Mvc.Models
{
    public class GradingModel
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public ExamServer.EntityFramework.Entities.Exam Exam { get; set; }
        public double Grade { get; set; }
        public List<Error> Errors { get; set; }
        public int NumberOfQuestions { get; set; }

        public void CalculateGrade()
        {
            var correctAnswers = NumberOfQuestions - Errors.Count();

            double result = (correctAnswers / Errors.Count()) * 100;
            Grade = result;
        }

    }
}
