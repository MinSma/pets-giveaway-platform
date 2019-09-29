using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Users.Commands.PutUpdateUser
{
    class PutUpdateUserCommandHandler : IRequestHandler<PutUpdateUserCommand, Unit>
    {
        public Task<Unit> Handle(PutUpdateUserCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
