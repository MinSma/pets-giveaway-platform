using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Exceptions;
using PGP.Persistence;

namespace PGP.Application.Comments.Commands.PutUpdateComment
{
    public class PutUpdateCommentCommandHandler : IRequestHandler<PutUpdateCommentCommand, Unit>
    {
        private readonly IPGPDbContext _context;

        public PutUpdateCommentCommandHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(PutUpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (comment == null)
            {
                throw new NotFoundException($"Comment id {request.Id} not exists.");
            }

            comment.Text = request.Text;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
