using MediatR;
using System.Collections.Generic;

namespace PGP.Application.Pets.Queries.GetAllPetsByCategoryId
{
    public class GetAllPetsByCategoryIdQuery : IRequest<List<GetAllPetsByCategoryIdQueryResponse>>
    {
        public int CategoryId { get; set; }
    }
}
