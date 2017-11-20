using GC.Core.Entities;
using GC.Core.Interfaces;
using GC.Core.Interfaces.Repositories;
using GC.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GC.Data.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(GCContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }

        public override async Task<Company> GetByIdAsync(int id, IEnumerable<string> includePaths = null)
        {
            if (includePaths == null)
                return await base.context.Set<Company>().FindAsync(id);

            return await base.context.Set<Company>()
                .Include(v => v.Photos)
                .Include(v => v.ProvidedServices)
                .SingleOrDefaultAsync(i => i.Id == id);
        }
    }
}
