using MediatR;
using System.Collections.Generic;

namespace PGP.Application.Pets.Queries.GetAllPets
{
    public class GetAllPetsQuery : IRequest<List<GetAllPetsQueryResponse>>
    {
    }
}
