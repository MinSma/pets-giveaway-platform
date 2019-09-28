using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Pets.Queries.GetAllPets
{
    public class GetAllPetsQueryHandler : IRequestHandler<GetAllPetsQuery, List<GetAllPetsQueryResponse>>
    {
        public Task<List<GetAllPetsQueryResponse>> Handle(GetAllPetsQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
