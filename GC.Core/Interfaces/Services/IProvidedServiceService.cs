using System.Threading.Tasks;
using GC.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace GC.Core.Interfaces.Services
{
    public interface IProvidedServiceService : IServiceBase<ProvidedService>
    {
        Task<Photo> UploadCoverlPicture(ProvidedService providedService, IFormFile file, string uploadsFolderPath);
        Task RemoveCoverPicture(ProvidedService providedService, string uploadsFolderPath);

        Task<Photo> UploadThumbnaillPicture(ProvidedService providedService, IFormFile file, string uploadsFolderPath);
        Task RemoveThumbnailPicture(ProvidedService providedService, string uploadsFolderPath);
    }
}
