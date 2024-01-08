using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InterviewBallast.Infrastructure.IRepositories
{
    public interface IBaseRepository<T, TContext> where T : class where TContext : DbContext
    {
        Task Remove(params object[] keys);
        Task Update(T entity);
        Task<T?> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task SaveChangesAsync();
    }
}
