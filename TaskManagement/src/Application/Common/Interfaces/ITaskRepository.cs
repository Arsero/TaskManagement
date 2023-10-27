namespace Application.Common.Interfaces
{
    public interface ITaskRepository : IGenericRepository<Domain.Entities.Task>
    {
        Task<bool> TaskExist(int id);
    }
}
