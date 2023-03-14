using ExamManagement.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
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

        public async Task<Exam> GetExamByName(string name)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7129/api/exam");

            var exam = await response.Content.ReadFromJsonAsync<Exam>();

            return exam;

        }
        public void AddExam(Exam exam)
        {
            SaveExamToJson(exam, Path);

        }
        public async Task UpdateExam(Exam exam)
        {
            if(exam != null)
            {

            }
            // Read the exam data from the local JSON file
            string json = File.ReadAllText(Path + "/exams.json");
            List<Exam> exams = JsonConvert.DeserializeObject<List<Exam>>(json);
            if(exam != null)
            {
                // Update the exam data
                int index = exams.FindIndex(e => e.Id == exam.Id);
                exams[index] = exam;
            }
           

            // Serialize the updated exam data to JSON
            json = JsonConvert.SerializeObject(exams, Formatting.Indented);

            // Write the JSON back to the file
            File.WriteAllText(Path + "/exams.json", json);


            var response = await _httpClient.PostAsJsonAsync<List<Exam>>($"https://localhost:7129/api/exam", exams);

            var content = await response.Content.ReadFromJsonAsync<Exam>();

            
        
        }

        public async Task SubmitExamResult(ExamResult result)
        {
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7129/api/result", result);

            var content = await response.Content.ReadFromJsonAsync<List<ExamResult>>();

        }
        public async Task<List<ExamResult>> GetExamsStatistics(int examId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7129/api/results/examId");

            var content = await response.Content.ReadFromJsonAsync<List<ExamResult>>();
            return content;
        }

        private void SaveExamToJson(Exam exam, string filePath)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            if (File.Exists(filePath + "/exams.json"))
            {
                string json = File.ReadAllText(Path + "/exams.json");
                List<Exam> exams = JsonConvert.DeserializeObject<List<Exam>>(json);

                // Update the exam data
                int index = exams.FindIndex(e => e.Id == exam.Id);
                exams[index] = exam;

                // Serialize the updated exam data to JSON
                json = JsonConvert.SerializeObject(exams, Formatting.Indented);

                // Write the JSON back to the file
                File.WriteAllText(Path + "/exams.json", json);
            }
            else
            {
                string examJson = JsonConvert.SerializeObject(exam, settings);

                using (StreamWriter writer = File.CreateText(filePath + "/exams.json"))
                {
                    writer.Write(examJson);
                }
            }
          
        }
    }
}
