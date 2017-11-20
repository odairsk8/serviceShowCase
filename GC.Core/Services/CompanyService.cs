using GC.Core.Entities;
using GC.Core.Interfaces;
using GC.Core.Interfaces.Repositories;
using GC.Core.Interfaces.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GC.Core.Services
{
    public class CompanyService : ServiceBase<Company>, ICompanyService
    {
        private readonly IRepositoryBase<ProvidedService> _providedServiceRepository;

        public CompanyService(ICompanyRepository repository, IRepositoryBase<ProvidedService> providedServiceRepository)
        : base(repository)
        {
            base._repository = repository;
            this._providedServiceRepository = providedServiceRepository;
        }
    }
}
