using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PGP.Domain.Entities;

namespace PGP.Persistence.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(p => p.Text)
                .IsRequired();

            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.CreatedByUserId)
                .IsRequired();

            builder.Property(p => p.PetId)
                .IsRequired();

            builder.HasOne(p => p.CreatedByUser)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.CreatedByUserId);

            builder.HasOne(p => p.Pet)
                .WithMany(p => p.Comments)
                .HasForeignKey(p => p.PetId);
        }
    }
}
