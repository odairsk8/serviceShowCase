using GC.Core.Entities;
using System.Threading.Tasks;

namespace GC.Core.Interfaces.Repositories
{
    public interface IProvidedServiceRepository : IRepositoryBase<ProvidedService>
    {
        Task<ProvidedService> GetFullEntity(int id);
    }
}
