using GC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GC.Data.Configuration
{
    public class IncludedFeatureConfiguration : IEntityTypeConfiguration<IncludedFeature>
    {
        public void Configure(EntityTypeBuilder<IncludedFeature> builder)
        {
            builder.HasMany<Feature>(c => c.Features).WithOne();
            builder.HasOne<Photo>(c => c.FeaturePicture);
        }
    }
}
