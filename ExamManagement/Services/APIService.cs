using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace ExamManagement.Services
{
    public class APIService<ExternalData>
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _resource;
        public APIService(string baseUrl)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _resource = nameof(ExternalData);
        }

        public async Task<List<ExternalData>> GetData()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/{_resource}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"External API returned {response.StatusCode} status code.");
            }

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<ExternalData>>(content);
        }

        public async Task<ExternalData> GetDataById(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/api/{_resource}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"External API returned {response.StatusCode} status code.");
            }

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ExternalData>(content);
        }

        public async Task<int> AddData(ExternalData data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_baseUrl}/api/{_resource}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"External API returned {response.StatusCode} status code.");
            }

            var result = await response.Content.ReadAsStringAsync();

            return int.Parse(result);
        }

        public async Task<int> UpdateData(int id, ExternalData data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_baseUrl}/api/{_resource}/{id}", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"External API returned {response.StatusCode} status code.");
            }

            var result = await response.Content.ReadAsStringAsync();

            return int.Parse(result);
        }

        public async Task<int> RemoveData(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/api/{_resource}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"External API returned {response.StatusCode} status code.");
            }

            var result = await response.Content.ReadAsStringAsync();

            return int.Parse(result);
        }
    }
}
