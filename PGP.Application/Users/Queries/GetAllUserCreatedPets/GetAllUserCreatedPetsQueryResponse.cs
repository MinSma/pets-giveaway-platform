using PGP.Application.Models;
using PGP.Application.Pets;
using System;

namespace PGP.Application.Users.Queries.GetAllUserCreatedPets
{
    public class GetAllUserCreatedPetsQueryResponse : PetDto
    {
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public bool? IsSterilized { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public Option Category { get; set; }
    }
}
