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
    public class TestService : ITestService
    {
        private readonly HttpClient _httpClient;
        private string Path = Directory.GetCurrentDirectory() + "/Temp/file";

        public TestService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<List<Test>> GetExamsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Test>>("https://localhost:7129/api/exam");
        }

        public async Task<List<TestResult>> GetExamResultsByName(int id)
        {
            return await _httpClient.GetFromJsonAsync<List<TestResult>>($"https://localhost:7129/api/result/{id}");
        }

        public async Task<List<TestResult>> GetExamResults()
        {
            return await _httpClient.GetFromJsonAsync<List<TestResult>>("https://localhost:7129/api/result");
        }

        public async Task<Test> GetExamByName(string name)
        {
            return await _httpClient.GetFromJsonAsync<Test>($"https://localhost:7129/api/exam/{name}");
        }

        public void AddExam(Test exam)
        {
            SaveExamToJson(exam, Path);
        }

        public async Task PutToServer(Test exam)
        {
            await _httpClient.PutAsJsonAsync("https://localhost:7129/api/exam", exam);
        }

        public async Task UpdateExamJson(Test exam)
        {
            string json = File.ReadAllText(Path + "/exams.json");
            List<Test> exams = System.Text.Json.JsonSerializer.Deserialize<List<Test>>(json);
            var examToUpdate = exams.FirstOrDefault(s => s.Id == exam.Id);

            if (examToUpdate != null)
            {
                exams.Remove(examToUpdate);
                exams.Add(exam);
                json = System.Text.Json.JsonSerializer.Serialize(exams);
                File.WriteAllText(Path + "/exams.json", json);
            }
        }

        public async Task UpdateExam(Test exam, bool pushToServer)
        {
            await UpdateExamJson(exam);

            if (pushToServer)
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7129/api/exam", exam);
            }
        }

        public async Task SubmitExamResult(TestResult result)
        {
            await _httpClient.PostAsJsonAsync("https://localhost:7129/api/result", result);
        }

        public async Task<Test> GetExamsStatistics(Guid examId)
        {
            return await _httpClient.GetFromJsonAsync<Test>($"https://localhost:7129/api/results/{examId}");
        }

        private void SaveExamToJson(Test exam, string filePath)
        {
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

            if (File.Exists(Path + "/exams.json"))
            {
                string json = File.ReadAllText(Path + "/exams.json");
                List<Test> exams = System.Text.Json.JsonSerializer.Deserialize<List<Test>>(json);

                if (!exams.Any(s => s.Id == exam.Id))
                {
                    exams.Add(exam);
                }

                json = System.Text.Json.JsonSerializer.Serialize(exams);
                File.WriteAllText(Path + "/exams.json", json);
            }
            else
            {
                List<Test> examList = new List<Test> { exam };
                string examJson = System.Text.Json.JsonSerializer.Serialize(examList);

                File.WriteAllText(Path + "/exams.json", examJson);
            }
        }
    }
}
