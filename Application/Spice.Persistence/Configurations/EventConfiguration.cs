using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spice.Domain.Plants.Events;

namespace Spice.Persistence.Configurations
{
    internal sealed class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasIndex(x => x.Id);

            builder.Property(x => x.Description).HasMaxLength(500).IsRequired().IsUnicode();
            builder.Property(x => x.Occured).IsRequired();
            builder.Property(x => x.Type).IsRequired();

            builder.HasOne(x => x.Plant).WithMany(x => x.Events).IsRequired();
        }
    }
}