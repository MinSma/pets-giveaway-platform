using Microsoft.EntityFrameworkCore;
using PGP.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Persistence
{
    public interface IPGPDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Pet> Pets { get; set; }
        DbSet<Type> Types { get; set; }
        DbSet<Photo> Photos { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
