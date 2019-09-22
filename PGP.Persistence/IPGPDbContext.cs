using PGP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PGP.Persistence
{
    public interface IPGPDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Pet> Pets { get; set; }
        DbSet<Type> Types { get; set; }
        DbSet<Photo> Photos { get; set; }
    }
}
