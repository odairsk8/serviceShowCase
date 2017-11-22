using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GC.Web.DTOs
{
    public class ProvidedServiceFormDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CoverTitle { get; set; }
        public string Description { get; set; }

        public string ThumbnailTitle { get; set; }
        public string ThumbnailDescription { get; set; }

        public ICollection<IncludedFeatureFormDTO> IncludedFeatures {get;set;}

        public int CompanyId { get; set; }

        public ProvidedServiceFormDTO()
        {
            //this.IncludedFeatures = new List<IncludedFeatureFormDTO>();
        }
    }
}
