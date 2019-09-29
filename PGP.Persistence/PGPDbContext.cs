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

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PGPDbContext).Assembly);
        }
    }
}
