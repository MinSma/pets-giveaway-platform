using MediatR;
using PGP.Domain.Entities;
using PGP.Domain.Enums;
using PGP.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Pets.Commands.CreatePet
{
    public class CreatePetCommandHandler : IRequestHandler<CreatePetCommand, Unit>
    {
        private readonly IPGPDbContext _context;

        public CreatePetCommandHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreatePetCommand request, CancellationToken cancellationToken)
        {
            Pet pet = new Pet
            {
                Name = request.Name,
                Age = request.Age,
                Gender = request.Gender,
                Weight = request.Weight,
                Height = request.Height,
                IsSterilized = request.IsSterilized,
                Description = request.Description,
                DateAdded = DateTime.Now,
                State = State.NotGivenAway,
                CategoryId = request.CategoryId,
                PhotoCode = request.PhotoCode,
                UserId = request.UserId
            };

            await _context.Pets.AddAsync(pet, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
