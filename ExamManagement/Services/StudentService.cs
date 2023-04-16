using EasyTestMaker.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EasyTestMaker.Services
{
    public class StudentService : IStudentService
    {
        private readonly HttpClient _httpClient;
        private readonly TestService _examService;

        public StudentService()
        {
            _httpClient = new HttpClient();
            _examService = new TestService();
        }

        // Fetch list of students from the API
        public async Task<List<Student>> GetStudentsAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7129/api/student");
            return await response.Content.ReadFromJsonAsync<List<Student>>();
        }

        // Update student information using the API
        public async Task<Student> UpdateStudentInformation(Student student)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7129/api/student", student);
            return await response.Content.ReadFromJsonAsync<Student>();
        }

        // Validate if the exam can be started based on the start date and time
        public async Task<Test> StartExam(Test exam)
        {
            var currentDate = DateTime.Now;
            var examDate = exam.StartDateTime.Date;
            var examHour = exam.StartDateTime.Hour;
            var currentHour = currentDate.Hour;

            if (examDate != currentDate.Date || examHour != currentHour)
            {
                throw new InvalidOperationException("Your exam is not yet active or has expired");
            }

            return exam;
        }

        // Submit exam answers and return true if successful, false otherwise
        public async Task<bool> SubmitExamAnswers(Test exam, int studentId, StudentTest studentExam)
        {
            var gradingData = new GradingData(studentExam, studentId, exam);
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7129/api/result/", gradingData);
            return response.IsSuccessStatusCode;
        }

        // Save exam result by calling the ExamService
        public async Task SaveExamResult(int studentId, string studentName, Guid id, int grade, List<Error> errors)
        {
            var result = new TestResult(studentId, studentName, id, grade, errors);
            await _examService.SubmitExamResult(result);
        }
    }
}
