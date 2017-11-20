using GC.Core.Entities;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GC.Data.Migrations
{
    public partial class PopulateCompanyService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var dbContext = new DesignTimeDbContextFactory().CreateDbContext(null);
            var companies = dbContext.Set<Company>().Take(10);
            foreach (var item in companies)
            {
                for (int i = 0; i < 10; i++)
                {
                    item.ProvidedServices.Add(new Core.Entities.ProvidedService()
                    {
                        Name = Faker.TextFaker.Sentence(),
                        ThumbnailTitle = Faker.TextFaker.Sentence(),
                        ThumbnailDescription = Faker.TextFaker.Sentences(3)
                    });
                }
            }
            dbContext.SaveChanges();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var dbContext = new DesignTimeDbContextFactory().CreateDbContext(null);
            var companies = dbContext.Set<Company>().Take(10);
            foreach (var item in companies)
            {
                item.ProvidedServices = null;
            }
            dbContext.SaveChanges();
        }
    }
}
