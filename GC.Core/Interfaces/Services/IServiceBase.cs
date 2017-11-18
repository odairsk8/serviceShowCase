using GC.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GC.Core.Interfaces.Services
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        Task<QueryResult<TEntity>> GetByQueryAsync(IQueryObject<TEntity> query);
        Task<TEntity> GetByIdAsync(int id, IEnumerable<string> includePaths = null);
        Task SaveAsync();
        void Add(TEntity obj);
        Task DeleteAsync(TEntity obj);
    }
}
