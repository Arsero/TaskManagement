using System.Linq.Expressions;

namespace Application
{
    public interface IGenericRepository<T> where T : class
    {
        Task Add(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T?> GetById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        Task Update(T entity);
        Task Remove(T entity);
    }
}
