using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class TaskDbContext : DbContext
    {
        public DbSet<Domain.Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Task>()
                .HasMany<Comment>()
                .WithOne()
                .HasForeignKey(c => c.TaskId)
                .IsRequired();

            modelBuilder.Entity<Domain.Task>()
                .HasData(new Domain.Task { Id = 1, Title = "First task", Description = "Okay let's go !", DueDate = DateTime.Today.AddDays(1), IsCompleted = false },
            new Domain.Task { Id = 2, Title = "Second task", Description = "Okay let's go ! 2", DueDate = DateTime.Today.AddDays(2), IsCompleted = false },
            new Domain.Task { Id = 3, Title = "Third task", Description = "Okay let's go ! 3", DueDate = DateTime.Today.AddDays(3), IsCompleted = true });
        }
    }
}
