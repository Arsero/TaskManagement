namespace Application
{
    public interface ITaskService
    {
        public Task<IEnumerable<Domain.Task>> GetAllTasks();
        public void AddTask(Domain.Task task);
        public Task<Domain.Task?> GetTaskById(int id);
        public void CompleteTask(int id);
        public void UpdateTaskById(Domain.Task task);
        public void RemoveTaskById(int id);
        public Task<bool> IsTaskExists(int id);
    }
}
