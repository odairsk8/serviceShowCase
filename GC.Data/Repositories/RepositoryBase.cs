using GC.Core.Entities;
using GC.Core.Interfaces;
using GC.Core.Interfaces.Repositories;
using GC.Core.Querying;
using GC.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GC.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected GCContext context;
        private readonly IUnitOfWork unitOfWork;

        public RepositoryBase(GCContext context, IUnitOfWork unitOfWork)
        {
            this.context = context;
            this.unitOfWork = unitOfWork;
        }

        public void Add(TEntity obj)
        {
            this.context.Set<TEntity>().Add(obj);
            this.unitOfWork.Complete();
        }

        public async Task<QueryResult<TEntity>> GetByQueryAsync(IQueryObject<TEntity> query)
        {
            var queryResult = new QueryResult<TEntity>();
            var result = this.context.Set<TEntity>().AsQueryable();

            result = result.ApplyFiltering(query);
            queryResult.TotalItems = await result.CountAsync();

            result = result.ApplyOrdering(query);
            result = result.ApplyPaging(query);

            queryResult.Items = await result.ToListAsync();
            return queryResult;
        }

        public void Dispose()
        {
            if (this.context != null)
                this.context.Dispose();
        }
    }
}
