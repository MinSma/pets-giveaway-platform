using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<GetAllUsersQueryResponse>>
    {
        public Task<List<GetAllUsersQueryResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
