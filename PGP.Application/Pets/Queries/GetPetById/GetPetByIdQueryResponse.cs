using PGP.Domain.Entities;
using System.Collections.Generic;

namespace PGP.Application.Pets.Queries.GetPetById
{
    public class GetPetByIdQueryResponse : PetDto
    {
        public ICollection<Comment> Comments { get; set; }
    }
}
