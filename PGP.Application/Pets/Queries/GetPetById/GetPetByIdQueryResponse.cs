using PGP.Domain.Entities;
using PGP.Domain.Enums;
using System;
using System.Collections.Generic;

namespace PGP.Application.Pets.Queries.GetPetById
{
    public class GetPetByIdQueryResponse
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
        public ICollection<Photo> Photos { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
