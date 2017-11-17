using GC.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GC.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<GCContext>
    {
        public GCContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<GCContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("GC.Data"));
            return new GCContext(builder.Options);
        }
    }
}
