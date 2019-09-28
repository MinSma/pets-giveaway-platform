using Microsoft.EntityFrameworkCore;
using PGP.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Persistence
{
    public interface IPGPDbContext
    {
        DbSet<Category> Categories { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Like> Likes { get; set; }
        DbSet<Pet> Pets { get; set; }
        DbSet<Photo> Photos { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
