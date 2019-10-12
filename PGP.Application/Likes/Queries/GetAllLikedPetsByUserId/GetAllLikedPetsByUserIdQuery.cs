using MediatR;
using System.Collections.Generic;

namespace PGP.Application.Likes.Queries.GetAllLikedPetsByUserId
{
    public class GetAllLikedPetsByUserIdQuery : IRequest<List<GetAllLikedPetsByUserIdQueryResponse>>
    {
        public int UserId { get; set; }
    }
}
