using Application.Common.Interfaces.Repository;
using AutoFixture;
using AutoFixture.AutoMoq;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement.Tests.Common
{
    public class DatabaseFixture
    {
        private readonly TaskDbContext _dbContext;

        public DatabaseFixture()
        {
            _dbContext = CreateDbContext();
            TaskRepository = new TaskRepository(_dbContext);
        }

        public ITaskRepository TaskRepository { get; set; }

        private TaskDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var taskDbContext = new TaskDbContext(options);

            return taskDbContext;
        }
    }
}
