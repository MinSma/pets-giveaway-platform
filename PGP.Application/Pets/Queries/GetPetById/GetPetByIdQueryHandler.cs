using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Pets.Queries.GetPetById
{
    public class GetPetByIdQueryHandler : IRequestHandler<GetPetByIdQuery, GetPetByIdQueryResponse>
    {
        public Task<GetPetByIdQueryResponse> Handle(GetPetByIdQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
