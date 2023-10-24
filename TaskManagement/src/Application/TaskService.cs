namespace Application
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
        }

        public async Task<IEnumerable<Domain.Task>> GetAllTasks()
        {
            return await _taskRepository.GetAll();
        }

        public async Task AddTask(Domain.Task task)
        {
            await _taskRepository.Add(task);
        }

        public async Task<Domain.Task?> GetTaskById(int id)
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

        public async Task UpdateTaskById(int id, Domain.Task task)
        {
            if (task != null)
            {
                await _taskRepository.Update(task);
            }
        }

        public async Task RemoveTaskById(int id)
        {
            var task = await _taskRepository.GetById(id);
            if (task != null)
            {
                await _taskRepository.Remove(task);
            }
        }
    }
}
