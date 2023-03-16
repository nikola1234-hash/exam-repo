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
        /// <summary>
        /// Initializes a new instance of the ExamService class.
        /// </summary>
        public ExamService()
        {
            _httpClient = new HttpClient();
        }

        /// <summary>
        /// Gets the list of all exams asynchronously.
        /// </summary>
        /// <returns>The list of exams.</returns>
        public async Task<List<Exam>> GetExamsAsync()
        {
            var response = await _httpClient.GetAsync($"https://localhost:7129/api/exam");
            var results = await response.Content.ReadFromJsonAsync<List<Exam>>();
            return results;
        }

        /// <summary>
        /// Gets the list of exam results by exam ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the exam.</param>
        /// <returns>The list of exam results.</returns>
        public async Task<List<ExamResult>> GetExamResultsByName(int id)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7129/api/result/{id}");
            var results = await response.Content.ReadFromJsonAsync<List<ExamResult>>();
            return results;
        }

        /// <summary>
        /// Gets the list of all exam results asynchronously.
        /// </summary>
        /// <returns>The list of exam results.</returns>
        public async Task<List<ExamResult>> GetExamResults()
        {
            var response = await _httpClient.GetAsync($"https://localhost:7129/api/result");
            var results = await response.Content.ReadFromJsonAsync<List<ExamResult>>();
            return results;
        }

        /// <summary>
        /// Gets an exam by name asynchronously.
        /// </summary>
        /// <param name="name">The name of the exam.</param>
        /// <returns>The exam.</returns>
        public async Task<Exam> GetExamByName(string name)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7129/api/exam/{name}");
            var exam = await response.Content.ReadFromJsonAsync<Exam>();
            return exam;
        }

        /// <summary>
        /// Adds an exam to the local JSON file.
        /// </summary>
        /// <param name="exam">The exam to add.</param>
        public void AddExam(Exam exam)
        {
            SaveExamToJson(exam, Path);
        }

        /// <summary>
        /// Sends an exam to the server to update it asynchronously.
        /// </summary>
        /// <param name="exam">The exam to update.</param>
        public async Task PutToServer(Exam exam)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7129/api/exam", exam);
        }

        public async Task UpdateExamJson(Exam exam)
        {
            // Read the exam data from the local JSON file
            string json = File.ReadAllText(Path + "/exams.json");
            List<Exam> exams = JsonConvert.DeserializeObject<List<Exam>>(json);
            if (exam != null && !string.IsNullOrEmpty(exam.Name))
            {
                
                if (exams.Where(s => s.Id == exam.Id).FirstOrDefault() != null)
                {
                    var examToUpdate = exams.Where(s => s.Id == exam.Id).FirstOrDefault();
                    exams.Remove(examToUpdate);
                    // Update the exam data
                    examToUpdate = exam;
                    exams.Add(examToUpdate);
                }



                // Serialize the updated exam data to JSON
                json = JsonConvert.SerializeObject(exams, Formatting.Indented);
                File.WriteAllText(Path + "/exams.json", json);

            }
        }
        /// <summary>
        /// Updates an exam in the local JSON file and sends it to the server to update it asynchronously.
        /// </summary>
        /// <param name="exam">The exam to update.</param>
        /// <param name="pushToServer">A boolean value indicating whether to push the updated exam to the server.</param>
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

                if (!response.IsSuccessStatusCode)
                {
                    throw new ArgumentNullException("Push failed");
                }

            }


            
        }
        // This method submits an exam result to the server API at the specified endpoint
        // It accepts an ExamResult object as a parameter and returns a list of ExamResult objects as JSON
        public async Task SubmitExamResult(ExamResult result)
        {
            var response = await _httpClient.PostAsJsonAsync($"https://localhost:7129/api/result", result);

            var content = await response.Content.ReadFromJsonAsync<List<ExamResult>>();

        }
        // This method retrieves the exam statistics from the server API at the specified endpoint
        // It accepts an examId as a parameter and returns an Exam object as JSON
        public async Task<Exam> GetExamsStatistics(Guid examId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7129/api/results/{examId}");
            
            var content = await response.Content.ReadFromJsonAsync<Exam>();
            return content;
        }
        // This method saves an exam object to a JSON file at the specified file path
        // It accepts an Exam object and a file path as parameters and returns nothing
        private void SaveExamToJson(Exam exam, string filePath)
        {
            // Check if the directory exists, if not, create it
            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
       
            }

            // Use JSON.NET to serialize the Exam object to JSON
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            if (File.Exists(Path + "/exams.json"))
            {
                string json = File.ReadAllText(Path + "/exams.json");
                List<Exam> exams = JsonConvert.DeserializeObject<List<Exam>>(json);

                // Check if the Exam object already exists in the JSON file, if not, add it to the list of exams
                if (exams.Where(s => s.Id == exam.Id).FirstOrDefault() == null)
                {
                    exams.Add(exam);
                }

                // Serialize the updated list of exams to JSON
                json = JsonConvert.SerializeObject(exams, Formatting.Indented);

                // Write the JSON back to the file
                File.WriteAllText(Path + "/exams.json", json);
            }
            else
            {
                // If the JSON file does not exist, create a new one with the Exam object as the only item
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
