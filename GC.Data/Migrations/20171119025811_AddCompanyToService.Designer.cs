﻿// <auto-generated />
using GC.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace GC.Data.Migrations
{
    [DbContext(typeof(GCContext))]
    [Migration("20171119025811_AddCompanyToService")]
    partial class AddCompanyToService
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GC.Core.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Foundation");

                    b.Property<string>("History")
                        .HasMaxLength(1000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("GC.Core.Entities.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CompanyId");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("GC.Core.Entities.ProvidedService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyId");

                    b.Property<int?>("CoverImageId");

                    b.Property<string>("CoverTitle");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("ThumbnailDescription");

                    b.Property<int?>("ThumbnailPictureId");

                    b.Property<string>("ThumbnailTitle");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("CoverImageId");

                    b.HasIndex("ThumbnailPictureId");

                    b.ToTable("ProvidedService");
                });

            modelBuilder.Entity("GC.Core.Entities.Photo", b =>
                {
                    b.HasOne("GC.Core.Entities.Company")
                        .WithMany("Photos")
                        .HasForeignKey("CompanyId");
                });

            modelBuilder.Entity("GC.Core.Entities.ProvidedService", b =>
                {
                    b.HasOne("GC.Core.Entities.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GC.Core.Entities.Photo", "CoverImage")
                        .WithMany()
                        .HasForeignKey("CoverImageId");

                    b.HasOne("GC.Core.Entities.Photo", "ThumbnailPicture")
                        .WithMany()
                        .HasForeignKey("ThumbnailPictureId");
                });
#pragma warning restore 612, 618
        }
    }
}
