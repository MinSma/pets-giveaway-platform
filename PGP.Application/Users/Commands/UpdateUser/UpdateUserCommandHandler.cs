using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Exceptions;
using PGP.Application.Helpers;
using PGP.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Users.Commands.UpdateUser
{
    class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly IPGPDbContext _context;

        public UpdateUserCommandHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (user == null)
            {
                throw new NotFoundException($"User id {request.Id} not exists.");
            }

            user.Password = AuthHelper.GetPasswordHash(request.Password);
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;
            user.PhotoCode = request.PhotoCode;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
