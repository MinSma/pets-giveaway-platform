using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Exceptions;
using PGP.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Pets.Commands.PutUpdatePet
{
    public class PutUpdatePetCommandHandler : IRequestHandler<PutUpdatePetCommand, Unit>
    {
        private readonly IPGPDbContext _context;

        public PutUpdatePetCommandHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(PutUpdatePetCommand request, CancellationToken cancellationToken)
        {
            var pet = await _context.Pets
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (pet == null)
            {
                throw new NotFoundException($"Pet id {request.Id} not exists.");
            }

            pet.Name = request.Name;
            pet.Age = request.Age;
            pet.Gender = request.Gender;
            pet.Weight = request.Weight;
            pet.Height = request.Height;
            pet.IsSterilized = request.IsSterilized;
            pet.Description = request.Description;
            pet.CategoryId = request.CategoryId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
