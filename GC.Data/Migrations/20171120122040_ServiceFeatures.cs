using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GC.Data.Migrations
{
    public partial class ServiceFeatures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncludedFeature",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FeaturePictureId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProvidedServiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncludedFeature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncludedFeature_Photo_FeaturePictureId",
                        column: x => x.FeaturePictureId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncludedFeature_ProvidedService_ProvidedServiceId",
                        column: x => x.ProvidedServiceId,
                        principalTable: "ProvidedService",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feature",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IncludedFeatureId = table.Column<int>(nullable: false),
                    Name = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feature", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feature_IncludedFeature_IncludedFeatureId",
                        column: x => x.IncludedFeatureId,
                        principalTable: "IncludedFeature",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Feature_IncludedFeatureId",
                table: "Feature",
                column: "IncludedFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_IncludedFeature_FeaturePictureId",
                table: "IncludedFeature",
                column: "FeaturePictureId");

            migrationBuilder.CreateIndex(
                name: "IX_IncludedFeature_ProvidedServiceId",
                table: "IncludedFeature",
                column: "ProvidedServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feature");

            migrationBuilder.DropTable(
                name: "IncludedFeature");
        }
    }
}
