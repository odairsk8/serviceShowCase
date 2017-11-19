using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using GC.Core.Entities;

namespace GC.Core.Interfaces
{
    public interface IPhotoStorage
    {
        Task<string> StorePhoto(string uploadsFolderPath, IFormFile file);
        void removeFile(string fileName, string uploadsFolderPath);
    }
}
