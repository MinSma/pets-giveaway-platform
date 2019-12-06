using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Persistence;

namespace PGP.Application.Comments.Queries.GetAllComments
{
    class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, List<GetAllCommentsQueryResponse>>
    {
        private readonly IPGPDbContext _context;

        public GetAllCommentsQueryHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllCommentsQueryResponse>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Comments
                .Select(x => new GetAllCommentsQueryResponse
                {
                    Id = x.Id,
                    Text = x.Text,
                    CreatedAt = x.CreatedAt,
                    UserFullName = $"{x.CreatedByUser.FirstName} {x.CreatedByUser.LastName}",
                    UserEmail = x.CreatedByUser.Email
                })
                .ToListAsync(cancellationToken);
        }
    }
}
