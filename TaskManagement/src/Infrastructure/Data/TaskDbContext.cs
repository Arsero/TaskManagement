using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class TaskDbContext : DbContext
    {
        public DbSet<Domain.Entities.Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Task>()
                .HasMany<Comment>()
                .WithOne()
                .HasForeignKey(c => c.TaskId)
                .IsRequired();

            new TaskDbContextInitialiser(modelBuilder).Seed();
        }
    }
}
