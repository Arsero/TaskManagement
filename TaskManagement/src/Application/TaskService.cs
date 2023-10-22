﻿using Infrastructure;
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

        public async void AddTask(Domain.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task<Domain.Task?> GetTaskById(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async void CompleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
                {
                    task.IsCompleted = true;
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async void UpdateTaskById(Domain.Task task)
        {
            _context.Update(task);
            await _context.SaveChangesAsync();
        }

        public async void RemoveTaskById(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsTaskExists(int id)
        {
            return (await _context.Tasks.FindAsync(id)) is not null;
        }
    }
}
