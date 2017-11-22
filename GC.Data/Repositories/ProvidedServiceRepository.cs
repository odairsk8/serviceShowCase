using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GC.Core.Entities;
using GC.Core.Interfaces;
using GC.Core.Interfaces.Repositories;
using GC.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GC.Data.Repositories
{
    public class ProvidedServiceRepository : RepositoryBase<ProvidedService>, IProvidedServiceRepository
    {
        public ProvidedServiceRepository(GCContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public async Task<ProvidedService> GetFullEntity(int id)
        {
            return await this.context.Set<ProvidedService>()
                .Include(incFut => incFut.IncludedFeatures)
                    .ThenInclude(feat => feat.Features)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

    }
}
