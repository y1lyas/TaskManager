
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;


namespace TaskManager.Controllers
{
    [ApiController] 
    [Route("[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetByUserId(string userId)
        {
            return await _context.Tasks
                .Include(t => t.User)
                .Include(t => t.Project)
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.User)
                .Include(t => t.Project)
                .FirstOrDefaultAsync(t => t.Id == id);

            return task == null ? NotFound() : Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskItem task)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Kullanıcı kimliği artık string
            var userExists = await _context.Users.AnyAsync(u => u.Id == task.UserId);
            if (!userExists)
                ModelState.AddModelError(nameof(task.UserId), "Specified user does not exist");

            var projectExists = await _context.Projects.AnyAsync(p => p.Id == task.ProjectId);
            if (!projectExists)
                ModelState.AddModelError(nameof(task.ProjectId), "Specified project does not exist");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var newTask = await _context.Tasks
                .Include(t => t.User)
                .Include(t => t.Project)
                .FirstAsync(t => t.Id == task.Id);

            return CreatedAtAction(nameof(Get), new { id = task.Id }, new
            {
                newTask.Id,
                newTask.Title,
                User = new { newTask.User?.Id, newTask.User?.UserName },
                Project = new { newTask.Project?.Id, newTask.Project?.Name }
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskItem updatedTask)
        {
            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null)
                return NotFound("Task not found");

            if (existingTask.UserId != updatedTask.UserId &&
                !await _context.Users.AnyAsync(u => u.Id == updatedTask.UserId))
            {
                return BadRequest($"User with ID {updatedTask.UserId} not found");
            }

            if (existingTask.ProjectId != updatedTask.ProjectId &&
                !await _context.Projects.AnyAsync(p => p.Id == updatedTask.ProjectId))
            {
                return BadRequest($"Project with ID {updatedTask.ProjectId} not found");
            }

            existingTask.Title = updatedTask.Title;
            existingTask.Description = updatedTask.Description;
            existingTask.DueDate = updatedTask.DueDate;
            existingTask.IsCompleted = updatedTask.IsCompleted;
            existingTask.UserId = updatedTask.UserId;
            existingTask.ProjectId = updatedTask.ProjectId;

            await _context.SaveChangesAsync();
            return Ok(existingTask);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
