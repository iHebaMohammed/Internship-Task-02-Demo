﻿// <auto-generated />
using System;
using Demo.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Demo.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250116072058_Initial-Migration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Demo.Domain.Entities.InspectionPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("InspectionPlans");
                });

            modelBuilder.Entity("Demo.Domain.Entities.InspectionPlanKpis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("InspectionPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("KpiId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("InspectionPlanId");

                    b.HasIndex("KpiId");

                    b.ToTable("InspectionPlanKpis");
                });

            modelBuilder.Entity("Demo.Domain.Entities.InspectionPlanLocations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("InspectionPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("InspectionPlanId");

                    b.HasIndex("LocationId");

                    b.ToTable("InspectionPlanLocations");
                });

            modelBuilder.Entity("Demo.Domain.Entities.Kpi", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("InspectionPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("InspectionPlanId");

                    b.ToTable("Kpis");
                });

            modelBuilder.Entity("Demo.Domain.Entities.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("InspectionPlanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("InspectionPlanId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Demo.Domain.Entities.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("StartDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Demo.Domain.Entities.ProjectLocations", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectLocations");
                });

            modelBuilder.Entity("Demo.Domain.Entities.InspectionPlanKpis", b =>
                {
                    b.HasOne("Demo.Domain.Entities.InspectionPlan", "InspectionPlan")
                        .WithMany()
                        .HasForeignKey("InspectionPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Demo.Domain.Entities.Kpi", "Kpi")
                        .WithMany()
                        .HasForeignKey("KpiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InspectionPlan");

                    b.Navigation("Kpi");
                });

            modelBuilder.Entity("Demo.Domain.Entities.InspectionPlanLocations", b =>
                {
                    b.HasOne("Demo.Domain.Entities.InspectionPlan", "InspectionPlan")
                        .WithMany()
                        .HasForeignKey("InspectionPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Demo.Domain.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InspectionPlan");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("Demo.Domain.Entities.Kpi", b =>
                {
                    b.HasOne("Demo.Domain.Entities.InspectionPlan", null)
                        .WithMany("Kpis")
                        .HasForeignKey("InspectionPlanId");
                });

            modelBuilder.Entity("Demo.Domain.Entities.Location", b =>
                {
                    b.HasOne("Demo.Domain.Entities.InspectionPlan", null)
                        .WithMany("Locations")
                        .HasForeignKey("InspectionPlanId");
                });

            modelBuilder.Entity("Demo.Domain.Entities.ProjectLocations", b =>
                {
                    b.HasOne("Demo.Domain.Entities.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Demo.Domain.Entities.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("Demo.Domain.Entities.InspectionPlan", b =>
                {
                    b.Navigation("Kpis");

                    b.Navigation("Locations");
                });
#pragma warning restore 612, 618
        }
    }
}
