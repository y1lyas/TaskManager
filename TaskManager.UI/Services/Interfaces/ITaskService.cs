using TaskManager.Models;


namespace TaskManager.UI.Services.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task<bool> CreateAsync(TaskItem task);
        Task<bool> UpdateAsync(TaskItem task);
        Task<bool> DeleteAsync(int id);
        Task<List<TaskItem>> GetTasksByUserIdAsync(string userId);

    }
}
