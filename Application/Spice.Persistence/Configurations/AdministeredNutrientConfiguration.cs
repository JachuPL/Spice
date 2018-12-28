using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spice.Domain.Plants;

namespace Spice.Persistence.Configurations
{
    internal sealed class AdministeredNutrientConfiguration : IEntityTypeConfiguration<AdministeredNutrient>
    {
        public void Configure(EntityTypeBuilder<AdministeredNutrient> builder)
        {
            builder.ToTable("AdministeredNutrients");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.Id);

            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Date).IsRequired();

            builder.HasOne(x => x.Plant).WithMany(x => x.AdministeredNutrients).IsRequired();
            builder.HasOne(x => x.Nutrient).WithMany(x => x.AdministeredToPlants).IsRequired();
        }
    }
}