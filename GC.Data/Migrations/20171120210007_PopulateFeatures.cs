using GC.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GC.Data.Migrations
{
    public partial class PopulateFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var dbContext = new DesignTimeDbContextFactory().CreateDbContext(null);
            var companies = dbContext.Set<Company>().Include(c => c.ProvidedServices).Take(10);
            foreach (var item in companies)
            {
                foreach (var service in item.ProvidedServices)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        service.IncludedFeatures.Add(new IncludedFeature()
                        {
                            Name = i + "- " + Faker.TextFaker.Sentence(),
                            Features = new List<Feature>() {
                                new Feature(){ Name = Faker.StringFaker.AlphaNumeric(20) },
                                new Feature(){ Name = Faker.StringFaker.AlphaNumeric(20) },
                                new Feature(){ Name = Faker.StringFaker.AlphaNumeric(20) },
                                new Feature(){ Name = Faker.StringFaker.AlphaNumeric(20) },
                                new Feature(){ Name = Faker.StringFaker.AlphaNumeric(20) },
                                new Feature(){ Name = Faker.StringFaker.AlphaNumeric(20) },
                                new Feature(){ Name = Faker.StringFaker.AlphaNumeric(20) },
                                new Feature(){ Name = Faker.StringFaker.AlphaNumeric(20) }
                            }
                        });
                    }
                }
            }
            dbContext.SaveChanges();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
