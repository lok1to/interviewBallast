using InterviewBallast.Domain.Common;
using InterviewBallast.Infrastructure.Context;
using InterviewBallast.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace InterviewBallast.Infrastructure.Repositories
{
    public class BaseRepository<T, TContext> : IBaseRepository<T, TContext> where T : BaseEntity where TContext : DbContext
    {
        protected readonly TContext _dbContext;
        private readonly DbSet<T> _entitiySet;


        public BaseRepository(TContext dbContext)
        {
            _dbContext = dbContext;
            _entitiySet = _dbContext.Set<T>();
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbContext.AddAsync(entity, cancellationToken);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _entitiySet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _entitiySet.Where(expression).ToListAsync(cancellationToken);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await _entitiySet.FirstOrDefaultAsync(expression, cancellationToken);
        }

        public async Task Remove(params object[] keys)
        {
            var entity = await _entitiySet.FindAsync(keys);

            if (entity != null)
            {
                _entitiySet.Remove(entity);
            }
        }

        public async Task Update(T entity)
        {
            var existing = await _entitiySet.FindAsync(entity.Id);
            if (existing != null)
            {
                _dbContext.Entry(existing).CurrentValues.SetValues(entity);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
