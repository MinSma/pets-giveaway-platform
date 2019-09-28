using PGP.Domain.Entities;
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
                SeedTypes(context);
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

        private void SeedTypes(PGPDbContext context)
        {
            var types = new[]
            {
                new Category { Title = "Dogs" },
                new Category { Title = "Cats" },
                new Category { Title = "Others" }
            };

            context.Categories.AddRange(types);

            context.SaveChanges();
        }
    }
}
