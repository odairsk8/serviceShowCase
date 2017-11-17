using GC.Core.Entities;
using GC.Core.Interfaces;
using GC.Core.Interfaces.Repositories;
using GC.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GC.Core.Services
{
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repository;

        public ServiceBase(IRepositoryBase<TEntity> repository)
        {
            _repository = repository;
        }

        public void Add(TEntity obj)
        {
            _repository.Add(obj);
        }

        public async Task<QueryResult<TEntity>> GetByQueryAsync(IQueryObject<TEntity> query)
        {
            return await this._repository.GetByQueryAsync(query);
        }

        public void Dispose()
        {
            this._repository.Dispose();
        }
    }
}
