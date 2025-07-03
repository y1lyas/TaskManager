using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskManager.Models;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<Project> Projects { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // <-- Bu çok önemli: Identity şemasını ekler

        // Eğer özel kurallar varsa, onları da buraya yazarsın.
        // Örneğin:
        // builder.Entity<TaskItem>().HasOne(t => t.User).WithMany(u => u.Tasks);
    }

}
