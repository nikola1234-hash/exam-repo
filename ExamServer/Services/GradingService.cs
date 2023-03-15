using ExamServer.EntityFramework.Entities;
using ExamServer.Mvc.Models;

namespace ExamServer.Services
{
    public class GradingService
    {

        public ExamResult Grade(GradingData data)
        {
            var questions = data.Exam.Questions.ToList();
            int totalQuestions = data.Exam.Questions.Count;
            int correctAnswers = 0;
            List<Error> errors = new List<Error>();

            for (int i = 0; i < totalQuestions; i++)
            {
                Question question = questions[i];
                int selectedAnswerIndex = data.StudentExam.SelectedAnswers[i];
                int correctAnswerIndex = question.CorrectAnswerIndex;

                if (selectedAnswerIndex == correctAnswerIndex)
                {
                    correctAnswers++;
                }
                else
                {
                    errors.Add(new Error(question.Text, data.StudentExam.SelectedAnswers[i].ToString(), correctAnswerIndex.ToString()));
                }
            }

            int grade = (int)Math.Round((double)correctAnswers / totalQuestions * 100);
            return GetExamResult(data.StudentId, data.StudentExam.StudentName, data.Exam.Id, grade, errors);

        }

        public ExamResult GetExamResult(int studentId, string studentName, Guid id, int grade, List<Error> errors)
        {
            return new ExamResult(studentId, id, grade, errors);
        }
    
    }
}
