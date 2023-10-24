namespace Application
{
    public interface ITaskService
    {
        Task<IEnumerable<Domain.Task>> GetAllTasks();
        Task AddTask(Domain.Task task);
        Task<Domain.Task?> GetTaskById(int id);
        Task CompleteTask(int id);
        Task UpdateTaskById(int id, Domain.Task task);
        Task RemoveTaskById(int id);
    }
}
