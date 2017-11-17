using GC.Core.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GC.Data.Migrations
{
    public partial class PopulateCompanies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var dbContext = new DesignTimeDbContextFactory().CreateDbContext(null);
            for (int i = 0; i < 50; i++)
            {
                dbContext.Set<Company>().Add(new Core.Entities.Company()
                {
                    Name = Faker.CompanyFaker.Name(),
                    Foundation = Faker.DateTimeFaker.DateTime(),
                    History = Faker.TextFaker.Sentences(5)
                });
            }
            dbContext.SaveChanges();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
