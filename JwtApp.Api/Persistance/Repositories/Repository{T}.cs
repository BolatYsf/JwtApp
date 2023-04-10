using JwtApp.Api.Core.Application.Interfaces;
using JwtApp.Api.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Linq.Expressions;

namespace JwtApp.Api.Persistance.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, new()
    {
        private readonly JwtContext _context;
        private DbSet<T> _dbSet;

        public Repository(JwtContext context)
        {
            _context = context;
            _dbSet=_context.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
           return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByFilter(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.AsNoTracking().SingleOrDefaultAsync(filter);
        }

        public async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public  void Remove(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public void  Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
