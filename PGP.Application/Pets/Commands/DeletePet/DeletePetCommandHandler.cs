using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Common.Interfaces;
using PGP.Application.Exceptions;
using PGP.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Pets.Commands.DeletePet
{
    public class DeletePetCommandHandler : IRequestHandler<DeletePetCommand, Unit>
    {
        private readonly IPGPDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public DeletePetCommandHandler(IPGPDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeletePetCommand request, CancellationToken cancellationToken)
        {
            var pet = await _context.Pets
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (pet == null)
            {
                throw new NotFoundException($"Pet id {request.Id} not exists.");
            }

            if (_currentUserService.UserId != pet.UserId && !_currentUserService.Role.Equals("Admin"))
            {
                throw new UnauthorizedException("You can't do this action.");
            }

            _context.Pets.Remove(pet);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
