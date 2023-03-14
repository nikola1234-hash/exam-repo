using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Net.Http.Json;
using ExamManagement.Models;

namespace ExamManagement.Services
{
    public class APIService<T>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _resource;
        public APIService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
            _resource = typeof(T).Name;
        }

        public async Task<IEnumerable<T>> GetAllExamsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/{_resource}");
            response.EnsureSuccessStatusCode();

            var exams = await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
            
            return exams;
        }

        public async Task<T> GetExamAsync(string name)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/{_resource}/{name}");
            response.EnsureSuccessStatusCode();
            
            var exam = await response.Content.ReadFromJsonAsync<T>();

            return exam;
        }

        public async Task<T> CreateExamAsync(T exam)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/api/{_resource}", exam);
            response.EnsureSuccessStatusCode();

            var createdExam = await response.Content.ReadFromJsonAsync<T>();

            return createdExam;
        }

        public async Task UpdateExamAsync(int id, Task exam)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/{_resource}/{id}", exam);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteExamAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/{_resource}/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
