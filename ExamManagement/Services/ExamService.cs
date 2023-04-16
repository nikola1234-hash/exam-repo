using EasyTestMaker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace EasyTestMaker.Services
{
    public class ExamService : IExamService
    {
        private readonly HttpClient _httpClient;
        private string Path = Directory.GetCurrentDirectory() + "/Temp/file";

        public ExamService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Exam>> GetExamsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Exam>>("https://localhost:7129/api/exam");
        }

        public async Task<List<ExamResult>> GetExamResultsByName(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<ExamResult>>($"https://localhost:7129/api/result/{id}");
        }

        public async Task<List<ExamResult>> GetExamResults()
        {
            return await _httpClient.GetFromJsonAsync<List<ExamResult>>("https://localhost:7129/api/result");
        }

        public async Task<Exam> GetExamByName(string name)
        {
            return await _httpClient.GetFromJsonAsync<Exam>($"https://localhost:7129/api/exam/{name}");
        }

        public void AddExam(Exam exam)
        {
            SaveExamToJson(exam, Path);
        }

        public async Task PutToServer(Exam exam)
        {
            await _httpClient.PutAsJsonAsync("https://localhost:7129/api/exam", exam);
        }

        public async Task UpdateExamJson(Exam exam)
        {
            string json = File.ReadAllText(Path + "/exams.json");
            List<Exam> exams = System.Text.Json.JsonSerializer.Deserialize<List<Exam>>(json);
            var examToUpdate = exams.FirstOrDefault(s => s.Id == exam.Id);

            if (examToUpdate != null)
            {
                exams.Remove(examToUpdate);
                exams.Add(exam);
                json = System.Text.Json.JsonSerializer.Serialize(exams);
                File.WriteAllText(Path + "/exams.json", json);
            }
        }

        public async Task UpdateExam(Exam exam, bool pushToServer)
        {
            await UpdateExamJson(exam);

            if (pushToServer)
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7129/api/exam", exam);
            }
        }

        public async Task SubmitExamResult(ExamResult result)
        {
            await _httpClient.PostAsJsonAsync("https://localhost:7129/api/result", result);
        }

        public async Task<Exam> GetExamsStatistics(Guid examId)
        {
            return await _httpClient.GetFromJsonAsync<Exam>($"https://localhost:7129/api/results/{examId}");
        }

        private void SaveExamToJson(Exam exam, string filePath)
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            if (File.Exists(Path + "/exams.json"))
            {
                string json = File.ReadAllText(Path + "/exams.json");
                List<Exam> exams = System.Text.Json.JsonSerializer.Deserialize<List<Exam>>(json);

                if (!exams.Any(s => s.Id == exam.Id))
                {
                    exams.Add(exam);
                }

                json = System.Text.Json.JsonSerializer.Serialize(exams);
                File.WriteAllText(Path + "/exams.json", json);
            }
            else
            {
                List<Exam> examList = new List<Exam> { exam };
                string examJson = System.Text.Json.JsonSerializer.Serialize(examList);

                File.WriteAllText(Path + "/exams.json", examJson);
            }
        }
    }
}
