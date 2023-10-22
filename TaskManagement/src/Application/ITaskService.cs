namespace Application
{
    public interface ITaskService
    {
        public Task<IEnumerable<Domain.Task>> GetAllTasks();
        public Task AddTask(Domain.Task task);
        public Task<Domain.Task?> GetTaskById(int id);
        public Task CompleteTask(int id);
        public Task UpdateTaskById(Domain.Task task);
        public Task RemoveTaskById(int id);
        public Task<bool> IsTaskExists(int id);
    }
}
