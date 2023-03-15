using ExamManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ExamManagement.Services
{
    public class StudentService
    {
        private readonly HttpClient _httpClient;
        private readonly ExamService examService;
        public StudentService()
        {
            _httpClient = new HttpClient();
            examService = new ExamService();
        }

        // Update student information
        public async Task<Student> UpdateStudentInformation(Student student)
        {
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7129/api/student", student);
            var entity = await response.Content.ReadFromJsonAsync<Student>();
            return entity;
        }

        // Start exam
        public async Task<Exam> StartExam(Guid examId)
        {

            var response = await _httpClient.GetAsync($"https://localhost:7129/api/exam/{examId}");

            var exam = await response.Content.ReadFromJsonAsync<Exam>();
            
            if (exam.StartDateTime.Date == DateTime.Now.Date)
                return exam;
            return null;

        }

        // Submit exam answers
        public async Task SubmitExamAnswers(Exam exam, int studentId, StudentExam studentExam)
        {

            int totalQuestions = exam.Questions.Count;
            int correctAnswers = 0;
            List<Error> errors = new List<Error>();

            for (int i = 0; i < totalQuestions; i++)
            {
                Question question = exam.Questions[i];
                int selectedAnswerIndex = studentExam.SelectedAnswers[i];
                int correctAnswerIndex = question.CorrectAnswerIndex;

                if (selectedAnswerIndex == correctAnswerIndex)
                {
                    correctAnswers++;
                }
                else
                {
                    errors.Add(new Error(question.Text, studentExam.SelectedAnswers[i].ToString(), correctAnswerIndex.ToString()));
                }
            }

            int grade = (int)Math.Round((double)correctAnswers / totalQuestions * 100);

            // Save the grade and errors to the database

            await SaveExamResult(studentId, studentExam.StudentName, exam.Id, grade, errors);

            // Calculate grade and errors
            // Save ExamResult object to database
        }

        public async Task SaveExamResult(int studentId, string studentName, Guid id, int grade, List<Error> errors)
        {
            ExamResult result = new ExamResult(studentId, studentName, id, grade, errors);
            await examService.SubmitExamResult(result);
        }
    }
}