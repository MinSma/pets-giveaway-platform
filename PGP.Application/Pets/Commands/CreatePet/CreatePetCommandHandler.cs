using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Common.Interfaces;
using PGP.Application.Exceptions;
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
        private readonly ICurrentUserService _currentUserService;

        public CreatePetCommandHandler(IPGPDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(CreatePetCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId != request.UserId)
            {
                throw new UnauthorizedException("You can't do this action.");
            }

            var categoryExists = await _context.Categories.AnyAsync(x => x.Id == request.CategoryId);

            if (!categoryExists)
            {
                throw new NotFoundException($"User was not found with specified id = {request.CategoryId}");
            }

            Pet pet = new Pet
            {
                Name = request.Name,
                Age = request.Age,
                City = request.City,
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
