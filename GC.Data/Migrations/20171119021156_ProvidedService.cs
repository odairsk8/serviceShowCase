using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GC.Data.Migrations
{
    public partial class ProvidedService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProvidedService",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CoverImageId = table.Column<int>(nullable: true),
                    CoverTitle = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    ThumbnailDescription = table.Column<string>(nullable: true),
                    ThumbnailPictureId = table.Column<int>(nullable: true),
                    ThumbnailTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvidedService", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProvidedService_Photo_CoverImageId",
                        column: x => x.CoverImageId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProvidedService_Photo_ThumbnailPictureId",
                        column: x => x.ThumbnailPictureId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProvidedService_CoverImageId",
                table: "ProvidedService",
                column: "CoverImageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvidedService_ThumbnailPictureId",
                table: "ProvidedService",
                column: "ThumbnailPictureId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProvidedService");
        }
    }
}
