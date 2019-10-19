using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Common.Interfaces;
using PGP.Application.Exceptions;
using PGP.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Likes.Commands.DeleteLike
{
    public class DeleteLikeCommandHandler : IRequestHandler<DeleteLikeCommand, Unit>
    {
        private readonly IPGPDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public DeleteLikeCommandHandler(IPGPDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId != request.UserId)
            {
                throw new UnauthorizedException("You can't do this action.");
            }

            var userExists = await _context.Users.AnyAsync(x => x.Id == request.UserId);

            if (!userExists)
            {
                throw new NotFoundException($"User was not found with specified id = {request.UserId}");
            }

            var petExists = await _context.Pets.AnyAsync(x => x.Id == request.PetId);

            if (!petExists)
            {
                throw new NotFoundException($"Pet was not found with specified id = {request.PetId}");
            }

            var like = await _context.Likes
                .FirstOrDefaultAsync(x => x.UserId == request.UserId && x.PetId == request.PetId,
                cancellationToken);

            if (like == null)
            {
                throw new NotFoundException("Pet can't be unliked because it is not liked.");
            }

            _context.Likes.Remove(like);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
