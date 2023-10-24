namespace Application
{
    public interface ITaskRepository : IGenericRepository<Domain.Task>
    {
        Task<bool> TaskExist(int id);
    }
}
