using PGP.Application.Comments;
using System.Collections.Generic;

namespace PGP.Application.Pets.Queries.GetPetById
{
    public class GetPetByIdQueryResponse : PetDto
    {
        public ICollection<CommentDto> Comments { get; set; }
    }
}
