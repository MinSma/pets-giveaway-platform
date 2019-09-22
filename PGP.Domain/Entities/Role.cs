using System.Collections.Generic;

namespace PGP.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
