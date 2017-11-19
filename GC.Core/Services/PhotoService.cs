using GC.Core.Entities;
using GC.Core.Interfaces;
using GC.Core.Interfaces.Repositories;
using GC.Core.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GC.Core.Services
{
    public class PhotoService : ServiceBase<Photo>, IPhotoService
    {
        private readonly IPhotoRepository _repository;
        private readonly IPhotoStorage photoStorage;

        public PhotoService(IPhotoRepository repository, IPhotoStorage photoStorage)
        : base(repository)
        {
            this._repository = repository;
            this.photoStorage = photoStorage;
        }

        public async Task RemoveCompanyPhoto(Company company, Photo photo, string uploadsFolderPath)
        {
            this.photoStorage.removeFile(photo.FileName, uploadsFolderPath);
            company.Photos.Remove(photo);
            await this.SaveAsync();
        }

        public async Task<Photo> UploadCompanyPhoto(Company company, IFormFile file, string uploadsFolderPath)
        {
            var fileName = await this.photoStorage.StorePhoto(uploadsFolderPath, file);

            var photo = new Photo() { FileName = fileName };
            company.Photos.Add(photo);
            await base.SaveAsync();

            return photo;
        }
    }
}
