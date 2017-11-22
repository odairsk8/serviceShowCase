using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GC.Web.DTOs
{
    public class IncludedFeatureFormDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<KeyValuePairDTO> Features { get; set; }

        public int ProvidedServiceId { get; set; }

        public IncludedFeatureFormDTO()
        {
            this.Features = new List<KeyValuePairDTO>();
        }
    }
}
