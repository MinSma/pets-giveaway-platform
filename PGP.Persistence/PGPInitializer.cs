using PGP.Domain.Entities;
using PGP.Domain.Enums;
using System;
using System.Linq;
using Category = PGP.Domain.Entities.Category;

namespace PGP.Persistence
{
    public class PGPInitializer
    {
        public static void Initialize(PGPDbContext context)
        {
            var initializer = new PGPInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(PGPDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Roles.Any())
            {
                SeedRoles(context);
            }

            if (!context.Categories.Any())
            {
                SeedCategories(context);
            }

            if (!context.Users.Any())
            {
                SeedUsers(context);
            }

            if (!context.Pets.Any())
            {
                SeedPets(context);
            }

            if (!context.Comments.Any())
            {
                SeedComments(context);
            }
        }

        private void SeedRoles(PGPDbContext context)
        {
            var roles = new[]
            {
                new Role { Title = "User" },
                new Role { Title = "Moderator" },
                new Role { Title = "Admin" }
            };

            context.Roles.AddRange(roles);

            context.SaveChanges();
        }

        private void SeedCategories(PGPDbContext context)
        {
            var categories = new[]
            {
                new Category { Title = "Dogs" },
                new Category { Title = "Cats" },
                new Category { Title = "Others" }
            };

            context.Categories.AddRange(categories);

            context.SaveChanges();
        }

        private void SeedUsers(PGPDbContext context)
        {
            var users = new[]
            {
                new User
                {
                    Email = "petras.petraitis@email.com",
                    Password = "5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8",
                    PhoneNumber = "8654442121",
                    FirstName = "Petras",
                    LastName = "Petraitis",
                    RoleId = 2
                },
                new User
                {
                    Email = "jonas.jonaitis@email.com",
                    Password = "5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8",
                    PhoneNumber = "8653332121",
                    FirstName = "Jonas",
                    LastName = "Jonaitis",
                    RoleId = 3
                },
                new User
                {
                    Email = "paulius.paulauskas@email.com",
                    Password = "5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8",
                    PhoneNumber = "8652222121",
                    FirstName = "Paulius",
                    LastName = "Paulauskas",
                    RoleId = 1
                },
                new User
                {
                    Email = "admin@email.com",
                    Password = "5E884898DA28047151D0E56F8DC6292773603D0D6AABBDD62A11EF721D1542D8",
                    PhoneNumber = "8651112121",
                    FirstName = "Admin",
                    LastName = "Admin",
                    RoleId = 3
                },
            };

            context.Users.AddRange(users);

            context.SaveChanges();
        }

        private void SeedPets(PGPDbContext context)
        {
            var pets = new[]
            {
                new Pet
                {
                    Name = "Reksas",
                    Age = 5,
                    Gender = Gender.Male,
                    Weight = 30,
                    Height = 5.4,
                    IsSterilized = true,
                    Description = "Kažkoks aprašymas",
                    DateAdded = DateTime.Now,
                    State = State.NotGivenAway,
                    CategoryId = 1,
                    UserId = 4
                },
                new Pet
                {
                    Name = "Murkis",
                    Age = 3,
                    Gender = Gender.Male,
                    Weight = 30,
                    Height = 3.4,
                    IsSterilized = true,
                    Description = "Kažkoks aprašymas",
                    DateAdded = DateTime.Now,
                    State = State.NotGivenAway,
                    CategoryId = 2,
                    UserId = 4
                }
            };

            context.Pets.AddRange(pets);

            context.SaveChanges();
        }

        private void SeedComments(PGPDbContext context)
        {
            var comments = new[]
            {
                new Comment
                {
                    Text = "Pirmas komentaras",
                    CreatedAt = DateTime.Now,
                    CreatedByUserId = 4,
                    PetId = 1
                },
                new Comment
                {
                    Text = "Antras komentaras",
                    CreatedAt = DateTime.Now,
                    CreatedByUserId = 4,
                    PetId = 1
                },
                new Comment
                {
                    Text = "Kažkoks komentaras",
                    CreatedAt = DateTime.Now,
                    CreatedByUserId = 2,
                    PetId = 1
                },
                new Comment
                {
                    Text = "Komentaras",
                    CreatedAt = DateTime.Now,
                    CreatedByUserId = 4,
                    PetId = 2
                }
            };

            context.Comments.AddRange(comments);

            context.SaveChanges();
        }
    }
}
