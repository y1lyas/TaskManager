using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TaskManager.Models
{
    public class TaskItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        public DateTime DueDate { get; set; } = DateTime.Now.AddDays(1); // Default: tomorrow

        public bool IsCompleted { get; set; }
        public string UserId { get; set; } 
        public ApplicationUser? User { get; set; }

        [Required(ErrorMessage = "Project is required")]
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }

        public Project? Project { get; set; }
    }
}