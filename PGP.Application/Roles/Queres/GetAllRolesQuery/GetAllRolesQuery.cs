using MediatR;
using System.Collections.Generic;

namespace PGP.Application.Roles.Queries.GetRolesQuery
{
    public class GetAllRolesQuery : IRequest<List<GetAllRolesQueryResponse>>
    {
    }
}
