using System;
using System.Collections.Generic;
using System.Text;

namespace GC.Core.Entities
{
    public class ProvidedService
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Company Company { get; set; }
        public int CompanyId { get; set; }

        public string ThumbnailTitle { get; set; }
        public string ThumbnailDescription { get; set; }
        public int? ThumbnailPictureId { get; set; }
        public Photo ThumbnailPicture { get; set; }

        public string CoverTitle { get; set; }
        public string Description { get; set; }
        public Photo CoverImage { get; set; }
        public int? CoverImageId { get; set; }

    }
}
