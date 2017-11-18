using GC.Core.Entities;
using GC.Core.Interfaces;
using GC.Core.Interfaces.Repositories;
using GC.Data.Context;

namespace GC.Data.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(GCContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}
