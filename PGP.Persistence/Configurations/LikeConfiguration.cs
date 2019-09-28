using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PGP.Domain.Entities;

namespace PGP.Persistence.Configurations
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.Property(p => p.PetId)
                .IsRequired();

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.HasOne(p => p.Pet)
                .WithMany(p => p.Likes)
                .HasForeignKey(p => p.PetId);

            builder.HasOne(p => p.User)
                .WithMany(p => p.Likes)
                .HasForeignKey(p => p.UserId);
        }
    }
}
