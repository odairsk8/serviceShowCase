using GC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GC.Data.Configuration
{
    public class DomainTypeConfiguration : IEntityTypeConfiguration<DomainType>
    {
        public void Configure(EntityTypeBuilder<DomainType> builder)
        {
            throw new NotImplementedException();
        }
    }
}
