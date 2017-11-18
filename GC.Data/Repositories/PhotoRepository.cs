using GC.Core.Entities;
using GC.Core.Interfaces;
using GC.Core.Interfaces.Repositories;
using GC.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace GC.Data.Repositories
{
    public class PhotoRepository : RepositoryBase<Photo>, IPhotoRepository
    {
        public PhotoRepository(GCContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
        {
        }
    }
}
