namespace TaskManager.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
