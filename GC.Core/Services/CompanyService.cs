using GC.Core.Entities;
using GC.Core.Interfaces.Repositories;
using GC.Core.Interfaces.Services;

namespace GC.Core.Services
{
    public class CompanyService : ServiceBase<Company>, ICompanyService
    {
        private readonly ICompanyRepository _repository;

        public CompanyService(ICompanyRepository repository)
        : base(repository)
        {
            this._repository = repository;
        }
    }
}
