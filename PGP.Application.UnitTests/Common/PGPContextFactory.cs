using Microsoft.EntityFrameworkCore;
using PGP.Domain.Entities;
using PGP.Persistence;
using System;

namespace PGP.Application.UnitTests.Common
{
    public class PGPContextFactory
    {
        public static PGPDbContext Create()
        {
            var options = new DbContextOptionsBuilder<PGPDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new PGPDbContext(options);

            context.Database.EnsureCreated();

            var role = new Role { Title = "Admin" };

            context.Roles.Add(role);

            context.SaveChanges();

            context.Users.AddRange(new[]
            {
                new User
                {
                    Email = "email@email.com",
                    Password = "5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8",
                    PhoneNumber = "865342095",
                    FirstName = "Pirmas",
                    LastName = "Pirmas",
                    PhotoCode = "",
                    RoleId = role.Id
                },
                new User
                {
                    Email = "email1@email.com",
                    Password = "5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8",
                    PhoneNumber = "865342095",
                    FirstName = "Pirmas",
                    LastName = "Pirmas",
                    PhotoCode = "",
                    RoleId = role.Id
                }
            });

            context.SaveChanges();

            return context;
        }

        public static void Destroy(PGPDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
