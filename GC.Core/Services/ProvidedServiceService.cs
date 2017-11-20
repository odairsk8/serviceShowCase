using System.Collections.Generic;
using System.Threading.Tasks;
using GC.Core.Entities;
using GC.Core.Interfaces;
using GC.Core.Interfaces.Repositories;
using GC.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;

namespace GC.Core.Services
{
    public class ProvidedServiceService : ServiceBase<ProvidedService>, IProvidedServiceService
    {
        private readonly IRepositoryBase<Photo> photoRepository;
        private readonly IPhotoStorage photoStorage;
        

        public ProvidedServiceService(IRepositoryBase<ProvidedService> repository, 
            IRepositoryBase<Photo> photoRepository,
            IPhotoStorage photoStorage)
            : base(repository)
        {
            this.photoRepository = photoRepository;
            this.photoStorage = photoStorage;
        }

        public async Task RemoveCoverPicture(ProvidedService providedService, string uploadsFolderPath)
        {
            Photo oldPhoto = providedService.CoverImage ?? null;
            if (oldPhoto != null)
            {
                await this.photoRepository.DeleteAsync(oldPhoto);
                this.photoStorage.removeFile(oldPhoto.FileName, uploadsFolderPath);
                await base.SaveAsync();
            }
        }

        public async Task<Photo> UploadCoverPicture(ProvidedService providedService, IFormFile file, string uploadsFolderPath)
        {
            Photo oldPhoto = providedService.CoverImage ?? null;

            var newFile = await this.photoStorage.StorePhoto(uploadsFolderPath, file);
            var newPhoto = new Photo() { FileName = newFile };
            providedService.CoverImage = newPhoto;

            if (oldPhoto != null) {
                await this.photoRepository.DeleteAsync(oldPhoto);
                this.photoStorage.removeFile(oldPhoto.FileName, uploadsFolderPath);
            }

            await base.SaveAsync();
            return newPhoto;
        }
    }
}
