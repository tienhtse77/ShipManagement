using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class ShipConfiguration : IEntityTypeConfiguration<Ship>
    {
        public void Configure(EntityTypeBuilder<Ship> builder)
        {
            builder.Property(s => s.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
