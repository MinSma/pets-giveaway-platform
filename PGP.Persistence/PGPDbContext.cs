using Microsoft.EntityFrameworkCore;
using PGP.Domain.Entities;

namespace PGP.Persistence
{
    public class PGPDbContext : DbContext, IPGPDbContext
    {
        public PGPDbContext(DbContextOptions<PGPDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Photo> Photos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PGPDbContext).Assembly);
        }
    }
}
