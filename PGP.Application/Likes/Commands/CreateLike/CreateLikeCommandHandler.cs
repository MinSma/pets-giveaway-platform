using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Common.Interfaces;
using PGP.Application.Exceptions;
using PGP.Domain.Entities;
using PGP.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Likes.Commands.CreateLike
{
    public class CreateLikeCommandHandler : IRequestHandler<CreateLikeCommand, Unit>
    {
        private readonly IPGPDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public CreateLikeCommandHandler(IPGPDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
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
                var newlyCreatedLike = new Like
                {
                    PetId = request.PetId,
                    UserId = request.UserId
                };

                await _context.Likes.AddAsync(newlyCreatedLike, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            else
            {
                throw new ConflictException("This pet is already liked.");
            }
        }
    }
}
