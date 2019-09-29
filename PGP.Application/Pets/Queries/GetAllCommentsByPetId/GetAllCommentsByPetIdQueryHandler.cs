using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Pets.Queries.GetAllCommentsByPetId
{
    public class GetAllCommentsByPetIdQueryHandler : IRequestHandler<GetAllCommentsByPetIdQuery, List<GetAllCommentsByPetIdQueryResponse>>
    {
        private readonly IPGPDbContext _context;

        public GetAllCommentsByPetIdQueryHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllCommentsByPetIdQueryResponse>> Handle(GetAllCommentsByPetIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Comments
                .Include(x => x.CreatedByUser)
                .Where(x => x.PetId == request.Id)
                .Select(x => new GetAllCommentsByPetIdQueryResponse
                {
                    Id = x.Id,
                    Text = x.Text,
                    CreatedAt = x.CreatedAt,
                    CreatedByUser = x.CreatedByUser
                })
                .ToListAsync(cancellationToken);
        }
    }
}
