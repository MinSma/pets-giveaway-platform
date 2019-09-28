using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PGP.Domain.Entities;

namespace PGP.Persistence.Configurations
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.Property(p => p.Name)
                .HasMaxLength(25);

            builder.Property(p => p.Gender)
                .IsRequired();

            builder.Property(p => p.DateAdded)
                .IsRequired();

            builder.Property(p => p.State)
                .IsRequired();

            builder.Property(p => p.CategoryId)
                .IsRequired();

            builder.HasOne(p => p.Category)
                .WithMany(p => p.Pets)
                .HasForeignKey(p => p.CategoryId);

            builder.HasOne(p => p.User)
                .WithMany(p => p.Pets)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Comments)
                .WithOne(p => p.Pet)
                .HasForeignKey(p => p.PetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Likes)
                .WithOne(p => p.Pet)
                .HasForeignKey(p => p.PetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Photos)
                .WithOne(p => p.Pet)
                .HasForeignKey(p => p.PetId);
        }

    }
}
