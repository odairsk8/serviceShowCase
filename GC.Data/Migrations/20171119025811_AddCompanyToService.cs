using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GC.Data.Migrations
{
    public partial class AddCompanyToService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "ProvidedService",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProvidedService_CompanyId",
                table: "ProvidedService",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProvidedService_Company_CompanyId",
                table: "ProvidedService",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProvidedService_Company_CompanyId",
                table: "ProvidedService");

            migrationBuilder.DropIndex(
                name: "IX_ProvidedService_CompanyId",
                table: "ProvidedService");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ProvidedService");
        }
    }
}
