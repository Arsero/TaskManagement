namespace Application
{
    public interface ITaskService
    {
        Task<IEnumerable<Domain.Entities.Task>> GetAllTasks();
        Task AddTask(Domain.Entities.Task task);
        Task<Domain.Entities.Task?> GetTaskById(int id);
        Task CompleteTask(int id);
        Task UpdateTaskById(int id, Domain.Entities.Task task);
        Task RemoveTaskById(int id);
    }
}
