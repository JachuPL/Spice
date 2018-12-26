using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spice.Domain;

namespace Spice.Persistence.Configurations
{
    internal sealed class FieldConfiguration : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.ToTable("Fields");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired().IsUnicode();
            builder.Property(x => x.Description).HasMaxLength(500).IsUnicode();
            builder.Property(x => x.Latitude).IsRequired();
            builder.Property(x => x.Longtitude).IsRequired();

            builder.HasMany(x => x.Plants).WithOne(x => x.Field).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}