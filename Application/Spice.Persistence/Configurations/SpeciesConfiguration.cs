using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spice.Domain;

namespace Spice.Persistence.Configurations
{
    internal sealed class SpeciesConfiguration : IEntityTypeConfiguration<Species>
    {
        public void Configure(EntityTypeBuilder<Species> builder)
        {
            builder.ToTable("Species");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired().IsUnicode();
            builder.Property(x => x.LatinName).HasMaxLength(50).IsRequired().IsUnicode();
            builder.Property(x => x.Description).HasMaxLength(500).IsUnicode();

            builder.HasMany(x => x.Plants).WithOne(x => x.Species).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}