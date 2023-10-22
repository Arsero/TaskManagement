using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Application
{
    public class TaskService : ITaskService
    {
        private readonly TaskDbContext _context;

        public TaskService(TaskDbContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Domain.Task>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<int> AddTask(Domain.Task task)
        {
            _context.Tasks.Add(task);
            return await _context.SaveChangesAsync();
        }

        public async Task<Domain.Task?> GetTaskById(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<int> CompleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
                {
                    task.IsCompleted = true;
                    _context.Update(task);
                    return await _context.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<int> UpdateTaskById(Domain.Task task)
        {
            _context.Update(task);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveTaskById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(task);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> IsTaskExists(int id)
        {
            return (await _context.Tasks.FindAsync(id)) is not null;
        }
    }
}
