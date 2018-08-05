﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UklonTestApp.Structure.DataService.DataService;

namespace UklonTestApp.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20180804134604_LocalMigration")]
    partial class LocalMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846");

            modelBuilder.Entity("UklonTestApp.Models.Region", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RegionCode");

                    b.Property<string>("RegionName");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("UklonTestApp.Models.RegionTrafficStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("DateTimeNow");

                    b.Property<Guid?>("RegionId");

                    b.Property<string>("TrafficIcon");

                    b.Property<string>("TrafficLevel");

                    b.Property<string>("TrafficMessage");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("RegionTrafficStatuses");
                });

            modelBuilder.Entity("UklonTestApp.Models.RegionTrafficStatus", b =>
                {
                    b.HasOne("UklonTestApp.Models.Region", "Region")
                        .WithMany("RegionTrafficStatuses")
                        .HasForeignKey("RegionId");
                });
#pragma warning restore 612, 618
        }
    }
}