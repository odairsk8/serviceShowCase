using GC.Core.Entities;
using GC.Core.Interfaces;
using GC.Core.Interfaces.Repositories;
using GC.Core.Querying;
using GC.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        }

        public async Task SaveAsync()
        {
            await this.unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(TEntity obj)
        {
            this.context.Remove(obj);
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

        public virtual async Task<TEntity> GetByIdAsync(int id, IEnumerable<string> includePaths = null)
        {
            if(includePaths == null || includePaths.Count() == 0)
                return await this.context.Set<TEntity>().FindAsync(id);

            var set = this.context.Set<TEntity>();
            foreach (var item in includePaths)
                set.Include(item);

            return await set.FindAsync(id);            
        }

        public async Task<TEntity> GetByAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = this.context.Set<TEntity>().AsQueryable();
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    result = result.Include(include);
                }
            }
             return await result.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<TEntity>> FilterByAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            var result = this.context.Set<TEntity>().AsQueryable();
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    result = result.Include(include);
                }
            }
            return await result.Where(predicate).ToListAsync();
        }

        public void Dispose()
        {
            if (this.context != null)
                this.context.Dispose();
        }

        public void UpdateRange(IEnumerable<TEntity> obj)
        {
            this.context.UpdateRange(obj);
        }
    }
}
