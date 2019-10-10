using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Exceptions;
using PGP.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Pets.Commands.UpdatePet
{
    public class UpdatePetCommandHandler : IRequestHandler<UpdatePetCommand, Unit>
    {
        private readonly IPGPDbContext _context;

        public UpdatePetCommandHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePetCommand request, CancellationToken cancellationToken)
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
            pet.PhotoCode = request.PhotoCode;
            pet.CategoryId = request.CategoryId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
