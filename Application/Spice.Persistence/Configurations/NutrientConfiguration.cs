﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spice.Domain;

namespace Spice.Persistence.Configurations
{
    internal sealed class NutrientConfiguration : IEntityTypeConfiguration<Nutrient>
    {
        public void Configure(EntityTypeBuilder<Nutrient> builder)
        {
            builder.ToTable("Nutrients");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired().IsUnicode();
            builder.Property(x => x.Description).HasMaxLength(500).IsUnicode();
            builder.Property(x => x.DosageUnits).IsRequired().HasMaxLength(20).IsUnicode();

            builder.HasMany(x => x.AdministeredToPlants).WithOne(x => x.Nutrient).OnDelete(DeleteBehavior.Cascade);
        }
    }
}