using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TaskManager.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

        [Required]
        public int UserId { get; set; }

        [JsonIgnore] // JSON çıktısına dahil edilmez
        public User? User { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [JsonIgnore] // JSON çıktısına dahil edilmez
        public Project? Project { get; set; }
    }
}
