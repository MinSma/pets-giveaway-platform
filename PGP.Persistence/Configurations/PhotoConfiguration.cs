using PGP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PGP.Persistence.Configurations
{
    public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.Property(p => p.Url)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(p => p.DateAdded)
                .IsRequired();

            builder.Property(p => p.IsMain)
                .IsRequired();

            builder.Property(p => p.PublicId)
                .IsRequired()
                .HasMaxLength(60);

            builder.HasOne(p => p.Pet)
                .WithMany(p => p.Photos)
                .HasForeignKey(p => p.PetId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.User)
                .WithMany(p => p.Photos)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
