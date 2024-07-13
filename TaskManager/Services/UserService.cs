namespace TaskManager.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using TaskManager.Models;

    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:44382/") // Replace with your API base URL
            };
        }

        public async Task<List<UserDto>> GetUsersByBatchAsync(string batch)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"GetUsersByBatch/{batch}");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<UserDto>>(responseBody);
        }
    }

}
