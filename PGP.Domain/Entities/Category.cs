using System.Collections.Generic;

namespace PGP.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public ICollection<Pet> Pets { get; set; }
    }
}
