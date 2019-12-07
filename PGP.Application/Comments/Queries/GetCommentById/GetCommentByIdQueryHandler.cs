using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Exceptions;
using PGP.Persistence;

namespace PGP.Application.Comments.Queries.GetCommentById
{
    public class GetCommentByIdQueryHandler : IRequestHandler<GetCommentByIdQuery, GetCommentByIdQueryResponse>
    {
        private readonly IPGPDbContext _context;

        public GetCommentByIdQueryHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<GetCommentByIdQueryResponse> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
        {
            var comment = await _context.Comments
                .Where(x => x.Id == request.Id)
                .Select(x => new GetCommentByIdQueryResponse
                {
                    Id = x.Id,
                    Text = x.Text,
                    CreatedAt = x.CreatedAt,
                    UserId = x.CreatedByUserId,
                    PetId = x.PetId
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (comment == null)
            {
                throw new NotFoundException($"Comment id {request.Id} not exists.");
            }

            return comment;
        }
    }
}
