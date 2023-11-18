using Application.Common.Interfaces.Repository;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class TaskRepository : GenericRepository<Domain.Entities.Task>, ITaskRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TaskRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> TaskExist(int id)
        {
            return await _dbContext.Tasks
                .AsNoTracking()
                .AnyAsync(task => task.Id == id);
        }

        public async Task<IEnumerable<Domain.Entities.Task>> GetWithFilteringAndSorting(bool? filterComplete = null, bool? orderByDate = null, int pageNumber = 1, int pageSize = 10)
        {
            return await _dbContext.Tasks
                .WhereIf(filterComplete != null, t => t.IsCompleted == filterComplete)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .OrderByIf(orderByDate != null, t => t.DueDate)
                .ToListAsync();
        }
    }
}
