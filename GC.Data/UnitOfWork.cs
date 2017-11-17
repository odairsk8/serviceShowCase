using GC.Core.Interfaces;
using GC.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GC.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GCContext context;

        public UnitOfWork(GCContext context)
        {
            this.context = context;
        }

        public Task CompleteAsync()
        {
            return this.context.SaveChangesAsync();
        }

        public int Complete()
        {
            return this.context.SaveChanges();
        }
    }
}
