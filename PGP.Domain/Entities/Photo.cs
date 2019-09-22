using System;

namespace PGP.Domain.Entities
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }

        public int PetId { get; set; }
        public Pet Pet { get; set; }
    }
}
