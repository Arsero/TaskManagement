using Application;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class TaskRepository : GenericRepository<Domain.Entities.Task>, ITaskRepository
    {
        private readonly TaskDbContext _dbContext;

        public TaskRepository(TaskDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<bool> TaskExist(int id)
        {
            return await _dbContext.Tasks.AsNoTracking().AnyAsync(task => task.Id == id);
        }
    }
}
