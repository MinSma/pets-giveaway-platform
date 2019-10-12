using MediatR;
using PGP.Application.Common.Interfaces;
using PGP.Application.Exceptions;
using PGP.Domain.Entities;
using PGP.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Unit>
    {
        private readonly IPGPDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public CreateCommentCommandHandler(IPGPDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId != request.UserId)
            {
                throw new UnauthorizedException("You can't do this action.");
            }

            var comment = new Comment
            {
                Text = request.Text,
                PetId = request.PetId,
                CreatedByUserId = request.UserId
            };

            await _context.Comments.AddAsync(comment, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
