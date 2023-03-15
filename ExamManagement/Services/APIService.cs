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
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

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

            var exams = await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
            
            return exams;
        }

        public async Task<T> GetExamAsync(string name)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/{_resource}/{name}");
            
            var exam = await response.Content.ReadFromJsonAsync<T>();

            return exam;
        }
   
        public async Task<bool> CreateExamAsync(T exam)
        {

            var response = await _httpClient.PostAsJsonAsync<T>($"{_baseUrl}/api/{_resource}", exam);
            var t = await  response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public async Task UpdateExamAsync(int id, Task exam)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/api/{_resource}/{id}", exam);
        }

        public async Task DeleteExamAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/{_resource}/{id}");
        }
        public async Task<bool> Login(User user)
        {
            var response = await _httpClient.PostAsJsonAsync<User>($"{_baseUrl}/api/Authentication", user);
            var t = await response.Content.ReadAsStringAsync();
            var l = JsonConvert.DeserializeObject<User>(t);
            
            if (response.IsSuccessStatusCode)
            {
                Storage.Storage.UserId = l.Id;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
