using ExamManagement.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ExamManagement.Services
{
    public class ExamService
    {
        private readonly HttpClient _httpClient;
        private string Path = Directory.GetCurrentDirectory() + "/Temp/file";
        public ExamService()
        {
            _httpClient = new HttpClient();
        }


        public async Task<List<Exam>> GetExamsAsync()
        {
            var response = await _httpClient.GetAsync($"https://localhost:7129/api/exam");

            var results = await response.Content.ReadFromJsonAsync<List<Exam>>();

            return results;
        }

        public async Task<List<ExamResult>> GetExamResultsByName(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7129/api/result/{id}");

            var results = await response.Content.ReadFromJsonAsync<List<ExamResult>>();

            return results;

        }
        public async Task<List<ExamResult>> GetExamResults()
        {
            var response = await _httpClient.GetAsync($"https://localhost:7129/api/result");

            var results = await response.Content.ReadFromJsonAsync<List<ExamResult>>();

            return results;

        }
        public async Task<Exam> GetExamByName(string name)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7129/api/exam/{name}");

            var exam = await response.Content.ReadFromJsonAsync<Exam>();

            return exam;

        }
        public void AddExam(Exam exam)
        {
            SaveExamToJson(exam, Path);

        }
        public async Task UpdateExam(Exam exam, bool pushToServer)
        {
            // Read the exam data from the local JSON file
            string json = File.ReadAllText(Path + "/exams.json");
            List<Exam> exams = JsonConvert.DeserializeObject<List<Exam>>(json);
            if(exam != null && !string.IsNullOrEmpty(exam.Name))
            {
                if (exams.Where(s=> s.Id == exam.Id).FirstOrDefault()== null)
                {
                    // Update the exam data
                    exams.Add(exam);
                }
        


                // Serialize the updated exam data to JSON
                json = JsonConvert.SerializeObject(exams, Formatting.Indented);
                File.WriteAllText(Path + "/exams.json", json);

            }


            if (pushToServer)
            {

                var response = await _httpClient.PostAsJsonAsync<List<Exam>>($"https://localhost:7129/api/exam", exams);

                // var js = await response.Content.ReadAsStringAsync();

                //var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web); //note: cache and reuse this
                //var problemDetails = System.Text.Json.JsonSerializer.Deserialize<JsonProblems>(js, jsonOptions);
                if (!response.IsSuccessStatusCode)
                {
                    throw new ArgumentNullException("Push failed");
                }

            }


            
        }

        public async Task SubmitExamResult(ExamResult result)
        {
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7129/api/result", result);

            var content = await response.Content.ReadFromJsonAsync<List<ExamResult>>();

        }
        public async Task<Exam> GetExamsStatistics(Guid examId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7129/api/results/{examId}");
            
            var content = await response.Content.ReadFromJsonAsync<Exam>();
            return content;
        }

        private void SaveExamToJson(Exam exam, string filePath)
        {

            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
       
            }

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            if (File.Exists(Path + "/exams.json"))
            {
                string json = File.ReadAllText(Path + "/exams.json");
                List<Exam> exams = JsonConvert.DeserializeObject<List<Exam>>(json);

                if (exams.Where(s => s.Id == exam.Id).FirstOrDefault() == null)
                {
                    // Update the exam data
                    exams.Add(exam);
                }


                // Serialize the updated exam data to JSON
                json = JsonConvert.SerializeObject(exams, Formatting.Indented);

                // Write the JSON back to the file
                File.WriteAllText(Path + "/exams.json", json);
            }
            else
            {
                List<Exam> examList = new List<Exam>();
                examList.Add(exam);
                string examJson = JsonConvert.SerializeObject(examList, settings);

                using (StreamWriter writer = File.CreateText(Path + "/exams.json"))
                {
                    writer.Write(examJson);
                }
            }
          
        }
    }
}
