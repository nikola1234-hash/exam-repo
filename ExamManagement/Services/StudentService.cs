﻿using ExamManagement.Models;
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
            if (exam.StartDateTime.Date > DateTime.Now.Date)
            {
                throw new InvalidOperationException("Your exam is not yet active");
            }
            
            if (exam.StartDateTime.Date < DateTime.Now.Date)
            {
                throw new InvalidOperationException("Your exam has expired");
            }
            if (exam.StartDateTime.Hour < DateTime.Now.Hour)
            {
                throw new InvalidOperationException("Your exam has expired");
            }
            if (exam.StartDateTime.Hour > DateTime.Now.Hour)
            {
                throw new InvalidOperationException("Your exam is not yet active");
            }
         
           
            return exam;


            }
        // Submit exam answers
        public async Task<bool> SubmitExamAnswers(Exam exam, int studentId, StudentExam studentExam)
        {

            GradingData gradingData = new GradingData(studentExam, studentId, exam);

            var response = await _httpClient.PostAsJsonAsync<GradingData>($"https://localhost:7129/api/result/", gradingData);

            var post = await response.Content.ReadFromJsonAsync<Exam>();


            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
       
        }

        public async Task SaveExamResult(int studentId, string studentName, Guid id, int grade, List<Error> errors)
        {
            ExamResult result = new ExamResult(studentId, studentName, id, grade, errors);
            await examService.SubmitExamResult(result);
        }
    }
}