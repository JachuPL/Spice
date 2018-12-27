﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Spice.Persistence;

namespace Spice.Persistence.Migrations
{
    [DbContext(typeof(SpiceContext))]
    partial class SpiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Spice.Domain.Field", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .IsUnicode(true);

                    b.Property<double>("Latitude");

                    b.Property<double>("Longtitude");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Fields");
                });

            modelBuilder.Entity("Spice.Domain.Nutrient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .IsUnicode(true);

                    b.Property<string>("DosageUnits")
                        .HasMaxLength(20)
                        .IsUnicode(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Nutrients");
                });

            modelBuilder.Entity("Spice.Domain.Plants.Plant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Column");

                    b.Property<Guid>("FieldId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<DateTime>("Planted");

                    b.Property<int>("Row");

                    b.Property<Guid>("SpeciesId");

                    b.Property<int>("State");

                    b.HasKey("Id");

                    b.HasIndex("FieldId");

                    b.HasIndex("Id");

                    b.HasIndex("SpeciesId");

                    b.ToTable("Plants");
                });

            modelBuilder.Entity("Spice.Domain.Plants.Species", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .IsUnicode(true);

                    b.Property<string>("LatinName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("Id");

                    b.ToTable("Species");
                });

            modelBuilder.Entity("Spice.Domain.Plants.Plant", b =>
                {
                    b.HasOne("Spice.Domain.Field", "Field")
                        .WithMany("Plants")
                        .HasForeignKey("FieldId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Spice.Domain.Plants.Species", "Species")
                        .WithMany("Plants")
                        .HasForeignKey("SpeciesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
