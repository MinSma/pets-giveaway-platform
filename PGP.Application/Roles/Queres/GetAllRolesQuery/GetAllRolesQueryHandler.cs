using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Roles.Queries.GetRolesQuery
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<GetAllRolesQueryResponse>>
    {
        private readonly IPGPDbContext _context;

        public GetAllRolesQueryHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllRolesQueryResponse>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Roles
                .AsNoTracking()
                .Select(x => new GetAllRolesQueryResponse
                {
                    Id = x.Id,
                    Title = x.Title
                })
                .ToListAsync(cancellationToken);
        }
    }
}
