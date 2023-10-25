using Domain.Exceptions;

namespace Application
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Task>> GetAllTasks()
        {
            return await _taskRepository.GetAll();
        }

        public async Task AddTask(Domain.Entities.Task task)
        {
            await _taskRepository.Add(task);
        }

        public async Task<Domain.Entities.Task?> GetTaskById(int id)
        {
            return await _taskRepository.GetById(id);
        }

        public async Task CompleteTask(int id)
        {
            var task = await _taskRepository.GetById(id);
            if (task != null)
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday)
                {
                    task.IsCompleted = true;
                    await _taskRepository.Update(task);
                }
            }
        }

        public async Task UpdateTaskById(int id, Domain.Entities.Task task)
        {
            if (task != null)
            {
                await _taskRepository.Update(task);
            }
        }

        public async Task RemoveTaskById(int id)
        {
            var task = await _taskRepository.GetById(id) 
                ?? throw new Exception("Task not found.");

            await _taskRepository.Remove(task);
        }
    }
}
