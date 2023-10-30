using Application.Common.Interfaces.Repository;
using Infrastructure.Extensions;
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
            return await _dbContext.Tasks
                .AsNoTracking()
                .AnyAsync(task => task.Id == id);
        }

        public async Task<IEnumerable<Domain.Entities.Task>> GetWithFilteringAndSorting(bool? filterComplete = null, bool? orderByDate = null)
        {
            return await _dbContext.Tasks
                .AsNoTracking()
                .WhereIf(filterComplete != null, t => t.IsCompleted == filterComplete)
                .OrderByIf(orderByDate != null, t => t.DueDate)
                .ToListAsync();
        }
    }
}
