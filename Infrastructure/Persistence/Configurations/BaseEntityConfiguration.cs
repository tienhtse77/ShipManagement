using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(t => t.Id)
                   .IsRequired()
                   .HasMaxLength(36)
                   .ValueGeneratedOnAdd();

            builder.Property(t => t.Created)
                   .HasColumnName("Created")
                   .IsRequired();

            builder.Property(t => t.Updated)
                   .HasColumnName("Updated");
        }
    }
}
