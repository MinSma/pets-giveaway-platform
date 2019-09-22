﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PGP.Domain.Entities;

namespace PGP.Persistence.Configurations
{
    public class AnimalConfiguration : IEntityTypeConfiguration<Pet>
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

            builder.Property(p => p.TypeId)
                .IsRequired();

            builder.HasOne(p => p.Type)
                .WithMany(p => p.Pets)
                .HasForeignKey(p => p.TypeId);

            builder.HasOne(p => p.User)
                .WithMany(p => p.Pets)
                .HasForeignKey(p => p.UserId);

            builder.HasMany(p => p.Photos)
                .WithOne(p => p.Pet)
                .HasForeignKey(p => p.PetId);
        }

    }
}
