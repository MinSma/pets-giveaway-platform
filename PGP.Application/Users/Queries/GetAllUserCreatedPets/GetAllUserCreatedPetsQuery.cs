using MediatR;
using System.Collections.Generic;

namespace PGP.Application.Users.Queries.GetAllUserCreatedPets
{
    public class GetAllUserCreatedPetsQuery : IRequest<List<GetAllUserCreatedPetsQueryResponse>>
    {
    }
}
