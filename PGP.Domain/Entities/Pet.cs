using PGP.Domain.Enums;
using System;
using System.Collections.Generic;

namespace PGP.Domain.Entities
{
    public class Pet
    {
        public Pet()
        {
            Comments = new HashSet<Comment>();
            Likes = new HashSet<Like>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public int? Age { get; set; }
        public Gender Gender { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public bool? IsSterilized { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public State State { get; set; }
        public string PhotoCode { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
