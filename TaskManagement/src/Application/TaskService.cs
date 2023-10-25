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
            var task = await _taskRepository.GetById(id)
                ?? throw new NotFoundException("Task not found.");

            return task;
        }

        public async Task CompleteTask(int id)
        {
            var task = await _taskRepository.GetById(id)
                ?? throw new NotFoundException("Task not found.");

            task.Complete();
            await _taskRepository.Update(task);
        }

        public async Task UpdateTaskById(int id, Domain.Entities.Task task)
        {
            if (id != task.Id)
            {
                throw new ValidationException("Not the same IDs");
            }

            var taskExist = await _taskRepository.TaskExist(id);
            if(!taskExist)
            {
                throw new NotFoundException("Task not found.");
            }

            await _taskRepository.Update(task);
        }

        public async Task RemoveTaskById(int id)
        {
            var task = await _taskRepository.GetById(id) 
                ?? throw new NotFoundException("Task not found.");

            await _taskRepository.Remove(task);
        }
    }
}
