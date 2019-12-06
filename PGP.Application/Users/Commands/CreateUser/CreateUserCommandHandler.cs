using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Exceptions;
using PGP.Application.Helpers;
using PGP.Domain.Entities;
using PGP.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
    {
        private readonly IPGPDbContext _context;

        public CreateUserCommandHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _context.Users
                .AnyAsync(x => x.Email.ToLower().Equals(request.Email.ToLower()));

            if (userExists)
            {
                throw new ConflictException($"User with email: {request.Email} already exists.");
            }

            var user = new User
            {
                Password = AuthHelper.GetPasswordHash(request.Password),
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                RoleId = 1
            };

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}