using ExamManagement.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ExamManagement.Services
{
    public class StudentService
    {
        private readonly HttpClient _httpClient;
        private readonly ExamService examService;
        // Declare constructor
        public StudentService()
        {
            _httpClient = new HttpClient();
            examService = new ExamService();
        }

        // Get a list of students asynchronously
        public async Task<List<Student>> GetStudentsAsync()
        {
            var response = await _httpClient.GetAsync($"https://localhost:7129/api/student");
            var entity = await response.Content.ReadFromJsonAsync<List<Student>>();
            return entity;
        }

        // Update student information
        public async Task<Student> UpdateStudentInformation(Student student)
        {
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7129/api/student", student);
            var entity = await response.Content.ReadFromJsonAsync<Student>();
            return entity;
        }

        // Start exam
        public async Task<Exam> StartExam(Exam exam)
        {
            // Check if exam is active
            if (exam.StartDateTime.Date > DateTime.Now.Date)
            {
                throw new InvalidOperationException("Your exam is not yet active");
            }
            // Check if exam has expired
            if (exam.StartDateTime.Date < DateTime.Now.Date)
            {
                throw new InvalidOperationException("Your exam has expired");
            }
            // Check if exam has expired by hour
            if (exam.StartDateTime.Hour < DateTime.Now.Hour)
            {
                throw new InvalidOperationException("Your exam has expired");
            }
            // Check if exam is active by hour
            if (exam.StartDateTime.Hour > DateTime.Now.Hour)
            {
                throw new InvalidOperationException("Your exam is not yet active");
            }
            return exam;
        }

        // Submit exam answers
        public async Task<bool> SubmitExamAnswers(Exam exam, int studentId, StudentExam studentExam)
        {
            // Create grading data object
            GradingData gradingData = new GradingData(studentExam, studentId, exam);

            // Submit grading data
            var response = await _httpClient.PostAsJsonAsync<GradingData>($"https://localhost:7129/api/result/", gradingData);

            // Check if response is successful
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Save exam result
        public async Task SaveExamResult(int studentId, string studentName, Guid id, int grade, List<Error> errors)
        {
            // Create ExamResult object
            ExamResult result = new ExamResult(studentId, studentName, id, grade, errors);

            // Submit exam result
            await examService.SubmitExamResult(result);
        }
    }
}
