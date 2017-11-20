using System.Threading.Tasks;
using GC.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace GC.Core.Interfaces.Services
{
    public interface IProvidedServiceService : IServiceBase<ProvidedService>
    {
        Task<Photo> UploadCoverPicture(ProvidedService providedService, IFormFile file, string uploadsFolderPath);
        Task RemoveCoverPicture(ProvidedService providedService, string uploadsFolderPath);
    }
}
