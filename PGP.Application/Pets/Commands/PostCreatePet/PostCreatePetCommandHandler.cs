using MediatR;
using PGP.Domain.Entities;
using PGP.Domain.Enums;
using PGP.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Pets.Commands.PostCreatePet
{
    public class PostCreatePetCommandHandler : IRequestHandler<PostCreatePetCommand, Unit>
    {
        private readonly IPGPDbContext _context;

        public PostCreatePetCommandHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(PostCreatePetCommand request, CancellationToken cancellationToken)
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
                UserId = 4
            };

            await _context.Pets.AddAsync(pet, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
