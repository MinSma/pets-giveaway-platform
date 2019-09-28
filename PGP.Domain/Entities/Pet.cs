using PGP.Domain.Enums;
using System;
using System.Collections.Generic;

namespace PGP.Domain.Entities
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public Gender Gender { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public bool? IsSterilized { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public State State { get; set; }

        public int TypeId { get; set; }
        public Type Type { get; set; }

        public int CreatedByUserId { get; set; }
        public User CreatedByUser { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
