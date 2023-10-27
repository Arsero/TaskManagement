using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class TaskRepository : GenericRepository<Domain.Entities.Task>, ITaskRepository
    {
        private readonly TaskDbContext _dbContext;

        public TaskRepository(TaskDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> TaskExist(int id)
        {
            return await _dbContext.Tasks.AsNoTracking().AnyAsync(task => task.Id == id);
        }
    }
}
