using MediatR;
using System.Collections.Generic;

namespace PGP.Application.Roles.Queries.GetRoles
{
    public class GetAllRolesQuery : IRequest<List<GetAllRolesQueryResponse>>
    {
    }
}
