using PGP.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Type = PGP.Domain.Entities.Type;

namespace PGP.Persistence
{
    public class PGPInitializer
    {
        private readonly Dictionary<int, Role> Roles = new Dictionary<int, Role>();

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

            if (!context.Types.Any())
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
                new Type { Title = "Dogs" },
                new Type { Title = "Cats" },
                new Type { Title = "Others" }
            };

            context.Types.AddRange(types);

            context.SaveChanges();
        }
    }
}
