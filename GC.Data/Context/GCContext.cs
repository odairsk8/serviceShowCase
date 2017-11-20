using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using GC.Core.Entities;
using GC.Data.Configuration;
using System.Linq;

namespace GC.Data.Context
{
    public class GCContext : DbContext
    {

        public GCContext(DbContextOptions<GCContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Company>(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration<Photo>(new PhotoConfiguration());
            modelBuilder.ApplyConfiguration<ProvidedService>(new ProvidedServiceConfiguration());
        }


    }

    
}
