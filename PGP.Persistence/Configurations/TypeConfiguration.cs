using PGP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PGP.Persistence.Configurations
{
    public class TypeConfiguration : IEntityTypeConfiguration<Type>
    {
        public void Configure(EntityTypeBuilder<Type> builder)
        {
            builder.Property(p => p.Title)
                .HasMaxLength(25);

            builder.HasMany(p => p.Pets)
                .WithOne(p => p.Type)
                .HasForeignKey(p => p.TypeId);
        }
    }
}
