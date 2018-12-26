﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spice.Domain.Plants;

namespace Spice.Persistence.Configurations
{
    internal sealed class PlantConfiguration : IEntityTypeConfiguration<Plant>
    {
        public void Configure(EntityTypeBuilder<Plant> builder)
        {
            builder.ToTable("Plants");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired().IsUnicode();
            builder.Property(x => x.Specimen).HasMaxLength(50).IsRequired().IsUnicode();
            builder.Property(x => x.Row).IsRequired();
            builder.Property(x => x.Column).IsRequired();
            builder.Property(x => x.Planted).IsRequired();
            builder.Property(x => x.State).IsRequired();

            builder.HasOne(x => x.Field).WithMany(x => x.Plants).IsRequired();
        }
    }
}