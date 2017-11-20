using GC.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GC.Core.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : class
    {
        Task<QueryResult<TEntity>> GetByQueryAsync(IQueryObject<TEntity> query);
        void Add(TEntity obj);
        Task SaveAsync();
        Task<TEntity> GetByIdAsync(int id, IEnumerable<string> includePaths = null);
        Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task DeleteAsync(TEntity obj);
    }
}
