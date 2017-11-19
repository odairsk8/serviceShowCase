using GC.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace GC.Core.Interfaces.Services
{
    public interface IPhotoService : IServiceBase<Photo>, IDisposable
    {
        Task<Photo> UploadCompanyPhoto(Company company, IFormFile file, string uploadFolderPath);
        Task RemoveCompanyPhoto(Company company, Photo photo, string uploadsFolderPath);
    }
}
