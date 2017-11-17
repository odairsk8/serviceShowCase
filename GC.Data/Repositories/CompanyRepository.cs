using GC.Core.Entities;
using GC.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using GC.Data.Context;
using GC.Core.Interfaces;

namespace GC.Data.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(GCContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}
