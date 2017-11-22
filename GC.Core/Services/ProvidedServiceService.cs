using System.Collections.Generic;
using System.Threading.Tasks;
using GC.Core.Entities;
using GC.Core.Interfaces;
using GC.Core.Interfaces.Repositories;
using GC.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace GC.Core.Services
{
    public class ProvidedServiceService : ServiceBase<ProvidedService>, IProvidedServiceService
    {
        private readonly IProvidedServiceRepository providedServiceRepository;

        private readonly IRepositoryBase<Photo> photoRepository;
        private readonly IRepositoryBase<IncludedFeature> includedFeatureRepository;
        private readonly IPhotoStorage photoStorage;


        public ProvidedServiceService(IProvidedServiceRepository providedServiceRepository,
            IRepositoryBase<Photo> photoRepository,
            IRepositoryBase<IncludedFeature> includedFeatureRepository,
            IPhotoStorage photoStorage)
            : base(providedServiceRepository)
        {
            this.providedServiceRepository = providedServiceRepository;
            this.photoRepository = photoRepository;
            this.includedFeatureRepository = includedFeatureRepository;
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

        public async Task<Photo> UploadCoverlPicture(ProvidedService providedService, IFormFile file, string uploadsFolderPath)
        {
            Photo oldPhoto = providedService.CoverImage ?? null;

            var newFile = await this.photoStorage.StorePhoto(uploadsFolderPath, file);
            var newPhoto = new Photo() { FileName = newFile };
            providedService.CoverImage = newPhoto;

            if (oldPhoto != null)
            {
                await this.photoRepository.DeleteAsync(oldPhoto);
                this.photoStorage.removeFile(oldPhoto.FileName, uploadsFolderPath);
            }

            await base.SaveAsync();
            return newPhoto;
        }

        public async Task<Photo> UploadThumbnaillPicture(ProvidedService providedService, IFormFile file, string uploadsFolderPath)
        {
            Photo oldPhoto = providedService.ThumbnailPicture ?? null;

            var newFile = await this.photoStorage.StorePhoto(uploadsFolderPath, file);
            var newPhoto = new Photo() { FileName = newFile };
            providedService.ThumbnailPicture = newPhoto;

            if (oldPhoto != null)
            {
                await this.photoRepository.DeleteAsync(oldPhoto);
                this.photoStorage.removeFile(oldPhoto.FileName, uploadsFolderPath);
            }

            await base.SaveAsync();
            return newPhoto;
        }

        public async Task RemoveThumbnailPicture(ProvidedService providedService, string uploadsFolderPath)
        {
            Photo oldPhoto = providedService.ThumbnailPicture ?? null;
            if (oldPhoto != null)
            {
                await this.photoRepository.DeleteAsync(oldPhoto);
                this.photoStorage.removeFile(oldPhoto.FileName, uploadsFolderPath);
                await base.SaveAsync();
            }
        }

        public async Task<ProvidedService> GetFullEntity(int id)
        {
            return await this.providedServiceRepository.GetFullEntity(id);
        }

        public async Task<IEnumerable<IncludedFeature>> GetFeatures(int providedServiceId)
        {
            return await this.includedFeatureRepository.FilterByAsync(i => i.ProvidedServiceId == providedServiceId, c => c.Features);
        }

        public async Task SaveFeatures(int providedServiceId, IEnumerable<IncludedFeature> apiFeatures)
        {
            var dataBaseFeatures = await this.includedFeatureRepository.FilterByAsync(c => c.ProvidedServiceId == providedServiceId);

            var removedFeatures = dataBaseFeatures.Where(a => !apiFeatures.Select(c => c.Id).Contains(a.Id)).ToList();
            removedFeatures.ForEach(f =>
            {
                this.includedFeatureRepository.DeleteAsync(f);
            });           

            var addedFeatures = apiFeatures.Where(a => !dataBaseFeatures.Select(c => c.Id).Contains(a.Id)).ToList();
            addedFeatures.ForEach(f =>
            {
                f.Id = 0;
                f.ProvidedServiceId = providedServiceId;
                f.Features = f.Features.Select(n => new Feature() { Name = n.Name }).ToList();
                this.includedFeatureRepository.Add(f);
            });

            var updatedFeatures = dataBaseFeatures.Where(a => apiFeatures.Select(c => c.Id).Contains(a.Id)).ToList();
            updatedFeatures.ForEach(item =>
            {
                var target = apiFeatures.SingleOrDefault(c => c.Id == item.Id);
                updatedFeatures[updatedFeatures.IndexOf(item)] = target;
            });
            

            await this.SaveAsync();
        }
    }
}
