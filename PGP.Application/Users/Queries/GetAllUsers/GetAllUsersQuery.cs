using MediatR;
using System.Collections.Generic;

namespace PGP.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<GetAllUsersQueryResponse>>
    {
    }
}
