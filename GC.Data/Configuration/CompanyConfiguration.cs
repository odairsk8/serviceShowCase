using GC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GC.Data.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(c => c.Foundation).IsRequired();
            builder.Property(c => c.History).HasMaxLength(1000);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(255);
        }
    }
}
