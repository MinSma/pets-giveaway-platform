using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Exceptions;
using PGP.Domain.Entities;
using PGP.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Likes.Commands.PostLikeCreateCommand
{
    public class PostLikeCreateCommandHandler : IRequestHandler<PostLikeCreateCommand, Unit>
    {
        private readonly IPGPDbContext _context;

        public PostLikeCreateCommandHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(PostLikeCreateCommand request, CancellationToken cancellationToken)
        {
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
