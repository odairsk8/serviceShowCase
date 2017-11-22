using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GC.Core.Entities
{
    public class IncludedFeature
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Feature> Features { get; set; }
        public Photo FeaturePicture { get; set; }
        public int? FeaturePictureId { get; set; }

        public int ProvidedServiceId { get; set; }

        public IncludedFeature()
        {
            this.Features = new List<Feature>();
        }
    }
}
