using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Common.Interfaces;
using PGP.Application.Exceptions;
using PGP.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Pets.Commands.UpdatePet
{
    public class UpdatePetCommandHandler : IRequestHandler<UpdatePetCommand, Unit>
    {
        private readonly IPGPDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UpdatePetCommandHandler(IPGPDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdatePetCommand request, CancellationToken cancellationToken)
        {
            var pet = await _context.Pets
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (pet == null)
            {
                throw new NotFoundException($"Pet id {request.Id} not exists.");
            }

            if (_currentUserService.UserId != request.UserId && !_currentUserService.Role.Equals("Admin"))
            {
                throw new UnauthorizedException("You can't do this action.");
            }

            var categoryExists = await _context.Categories.AnyAsync(x => x.Id == request.CategoryId);

            if (!categoryExists)
            {
                throw new NotFoundException($"User was not found with specified id = {request.CategoryId}");
            }

            pet.Name = request.Name;
            pet.Age = request.Age;
            pet.Gender = request.Gender;
            pet.Weight = request.Weight;
            pet.Height = request.Height;
            pet.IsSterilized = request.IsSterilized;
            pet.Description = request.Description;
            pet.PhotoCode = request.PhotoCode;
            pet.CategoryId = request.CategoryId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
