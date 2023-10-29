namespace Application.Common.Interfaces
{
    public interface ITaskRepository : IGenericRepository<Domain.Entities.Task>
    {
        Task<bool> TaskExist(int id);
        Task<IEnumerable<Domain.Entities.Task>> GetWithFilteringAndSorting(bool? filterComplete = null, bool? orderByDate = null);
    }
}
