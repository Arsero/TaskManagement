using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class TaskDbContextInitialiser
    {
        private readonly ModelBuilder modelBuilder;

        public TaskDbContextInitialiser(ModelBuilder modelBuilder)
        {
            this.modelBuilder = modelBuilder;
        }

        public void Seed()
        {
            modelBuilder.Entity<Domain.Entities.Task>()
                .HasData(
                    new Domain.Entities.Task { Id = 1, Title = "First task", Description = "Okay let's go !", DueDate = DateTime.Today.AddDays(1), IsCompleted = false },
                    new Domain.Entities.Task { Id = 2, Title = "Second task", Description = "Okay let's go ! 2", DueDate = DateTime.Today.AddDays(2), IsCompleted = false },
                    new Domain.Entities.Task { Id = 3, Title = "Third task", Description = "Okay let's go ! 3", DueDate = DateTime.Today.AddDays(3), IsCompleted = true });
        }
    }
}
