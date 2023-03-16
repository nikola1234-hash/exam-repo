﻿using ExamManagement.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ExamManagement.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService()
        {
            _httpClient = new HttpClient();
        }
        // This method is used to authenticate the user by sending a POST request to the specified API endpoint.
        // It takes a User object as input and returns a boolean value indicating whether the login was successful or not.
        // It uses the HttpClient class to make the HTTP request.
        // If the response status code is successful, it stores the user's ID and username in the Storage class and returns true.
        // Otherwise, it returns false.
        public async Task<bool> Login(User user)
        {
            var response = await _httpClient.PostAsJsonAsync<User>($"https://localhost:7129/api/Authentication", user);
            var t = await response.Content.ReadAsStringAsync();
            var l = JsonConvert.DeserializeObject<User>(t);

            if (response.IsSuccessStatusCode)
            {
                Storage.Storage.UserId = l.Id;
                Storage.Storage.User = l.Username;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}