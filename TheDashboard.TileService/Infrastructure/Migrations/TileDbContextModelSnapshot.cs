﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheDashboard.TileService.Infrastructure;

#nullable disable

namespace TheDashboard.TileService.Infrastructure.Migrations
{
    [DbContext(typeof(TileDbContext))]
    partial class TileDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TheDashboard.TileService.Domain.Dashboard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Dashboards", (string)null);
                });

            modelBuilder.Entity("TheDashboard.TileService.Domain.Tile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid>("DashboardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("DataSource")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Icon")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("StaticText")
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("SubTitle")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<int?>("VisualizerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DashboardId");

                    b.HasIndex("VisualizerId");

                    b.ToTable("Tiles", (string)null);
                });

            modelBuilder.Entity("TheDashboard.TileService.Domain.Transformer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Template")
                        .HasMaxLength(8192)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Transformers", (string)null);
                });

            modelBuilder.Entity("TheDashboard.TileService.Domain.Visualizer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(512)
                        .HasColumnType("nvarchar(512)");

                    b.Property<bool>("Interactive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<long>("Refreshrate")
                        .HasColumnType("bigint");

                    b.Property<int?>("TransformerId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransformerId");

                    b.ToTable("Vizalizers", (string)null);
                });

            modelBuilder.Entity("TheDashboard.TileService.Domain.Tile", b =>
                {
                    b.HasOne("TheDashboard.TileService.Domain.Dashboard", "Dashboard")
                        .WithMany("Tiles")
                        .HasForeignKey("DashboardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TheDashboard.TileService.Domain.Visualizer", "Visualizer")
                        .WithMany("Tiles")
                        .HasForeignKey("VisualizerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Dashboard");

                    b.Navigation("Visualizer");
                });

            modelBuilder.Entity("TheDashboard.TileService.Domain.Visualizer", b =>
                {
                    b.HasOne("TheDashboard.TileService.Domain.Transformer", "Transformer")
                        .WithMany("Visualizers")
                        .HasForeignKey("TransformerId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Transformer");
                });

            modelBuilder.Entity("TheDashboard.TileService.Domain.Dashboard", b =>
                {
                    b.Navigation("Tiles");
                });

            modelBuilder.Entity("TheDashboard.TileService.Domain.Transformer", b =>
                {
                    b.Navigation("Visualizers");
                });

            modelBuilder.Entity("TheDashboard.TileService.Domain.Visualizer", b =>
                {
                    b.Navigation("Tiles");
                });
#pragma warning restore 612, 618
        }
    }
}
