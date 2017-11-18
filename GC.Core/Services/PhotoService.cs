using GC.Core.Entities;
using GC.Core.Interfaces.Repositories;
using GC.Core.Interfaces.Services;

namespace GC.Core.Services
{
    public class PhotoService : ServiceBase<Photo>, IPhotoService
    {
        private readonly IPhotoRepository _repository;

        public PhotoService(IPhotoRepository repository)
        : base(repository)
        {
            this._repository = repository;
        }
    }
}
