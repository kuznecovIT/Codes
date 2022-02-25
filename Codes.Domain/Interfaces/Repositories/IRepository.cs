using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Codes.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetAsync(object id);
        
        Task<IEnumerable<TEntity>> GetAllAsync();
        
        Task<IEnumerable<TEntity>> GetAllReadAsync();
        
        Task<int> ExecuteSqlRawAsync(string sql);
        
        Task<bool> HasDataAsync();
        
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        
        void AddRange(IEnumerable<TEntity> entity);
        
        void Update(TEntity entity);

        void UpdateRange(IEnumerable<TEntity> entity);
        
        void Remove(TEntity entity);
        
        void RemoveRange(IEnumerable<TEntity> entity);
    }
}