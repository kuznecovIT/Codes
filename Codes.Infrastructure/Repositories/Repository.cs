using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Codes.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Codes.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            Context = context;
            _dbSet = Context.Set<TEntity>();
        }
        
        /// <inheritdoc />
        public async Task<TEntity> GetAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        
        
        /// <inheritdoc />
        public async Task<int> ExecuteSqlRawAsync(string sql)
        {
            return await Context.Database.ExecuteSqlRawAsync(sql);
        }
        
        /// <inheritdoc />
        public Task<bool> HasDataAsync() =>
            _dbSet.AnyAsync();

        /// <inheritdoc />
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        /// <inheritdoc />
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        /// <inheritdoc />
        public void AddRange(IEnumerable<TEntity> entity)
        {
            _dbSet.AddRange(entity);
        }

        /// <inheritdoc />
        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        /// <inheritdoc />
        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        /// <inheritdoc />
        public void UpdateRange(IEnumerable<TEntity> entity)
        {
            _dbSet.UpdateRange(entity);
        }

        /// <inheritdoc />
        public void RemoveRange(IEnumerable<TEntity> entity)
        {
            _dbSet.RemoveRange(entity);
        }
    }
}