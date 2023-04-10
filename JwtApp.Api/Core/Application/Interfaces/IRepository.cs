using System.Linq.Expressions;

namespace JwtApp.Api.Core.Application.Interfaces
{
    public interface IRepository<T> where T : class,new()
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(object id);
        Task<T?> GetByFilter(Expression<Func<T, bool>> filter);

        Task CreateAsync(T entity);

        void Update(T entity);

        void Remove(T entity);
    }
}
