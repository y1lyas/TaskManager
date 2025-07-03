using System.Net.Http.Json;
using TaskManager.Models;
using TaskManager.UI.Services.Interfaces;


namespace TaskManager.UI.Services
{
    public class TaskService : ITaskService
    {
        private readonly HttpClient _httpClient;

        public TaskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<TaskItem>>("https://localhost:7061/TaskItem");
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<TaskItem>($"https://localhost:7061/TaskItem/{id}");
        }

        public async Task<bool> CreateAsync(TaskItem task)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7061/TaskItem", task);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(TaskItem task)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7061/TaskItem/{task.Id}", task);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7061/TaskItem/{id}");
            return response.IsSuccessStatusCode;
        }
        public async Task<List<TaskItem>> GetTasksByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7061/TaskItem/user/{userId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<TaskItem>>() ?? new List<TaskItem>();
            }

            return new List<TaskItem>();
        }

    }
}
