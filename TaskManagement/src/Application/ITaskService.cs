namespace Application
{
    public interface ITaskService
    {
        public Task<IEnumerable<Domain.Task>> GetAllTasks();
        public Task<int> AddTask(Domain.Task task);
        public Task<Domain.Task?> GetTaskById(int id);
        public Task<int> CompleteTask(int id);
        public Task<int> UpdateTaskById(Domain.Task task);
        public Task<int> RemoveTaskById(int id);
        public Task<bool> IsTaskExists(int id);
    }
}
