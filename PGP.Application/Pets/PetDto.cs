using PGP.Domain.Enums;
using System;

namespace PGP.Application.Pets
{
    public class PetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string City { get; set; }
        public Gender Gender { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public bool? IsSterilized { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public State State { get; set; }
        public string PhotoCode { get; set; }
    }
}
