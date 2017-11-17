using GC.Core.Entities;
using GC.Core.Querying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GC.Core.Interfaces.Services
{
    public interface IServiceBase<TEntity> where TEntity : class
    {
        Task<QueryResult<TEntity>> GetByQueryAsync(IQueryObject<TEntity> query);
        void Add(TEntity obj);
    }
}
