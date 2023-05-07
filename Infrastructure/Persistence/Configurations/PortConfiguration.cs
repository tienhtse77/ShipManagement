using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PortConfiguration : IEntityTypeConfiguration<Port>
    {
        public void Configure(EntityTypeBuilder<Port> builder)
        {
            builder.Property(p => p.Name)
               .HasMaxLength(200)
               .IsRequired();
        }
    }
}
