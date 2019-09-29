using PGP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RIS.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(p => p.PhoneNumber)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(p => p.RoleId)
                .IsRequired();

            builder.HasOne(p => p.Role)
                .WithMany(p => p.Users)
                .HasForeignKey(p => p.RoleId);

            builder.HasMany(p => p.Comments)
                .WithOne(p => p.CreatedByUser)
                .HasForeignKey(p => p.CreatedByUserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Likes)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Pets)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.Photos)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
