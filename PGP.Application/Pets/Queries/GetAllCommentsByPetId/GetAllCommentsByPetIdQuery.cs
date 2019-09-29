using MediatR;
using System.Collections.Generic;

namespace PGP.Application.Pets.Queries.GetAllCommentsByPetId
{
    public class GetAllCommentsByPetIdQuery : IRequest<List<GetAllCommentsByPetIdQueryResponse>>
    {
        public int Id { get; set; }
    }
}
