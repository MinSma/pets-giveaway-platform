using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Users.Commands.PostCreateUser
{
    public class PostCreateUserCommandHandler : IRequestHandler<PostCreateUserCommand, Unit>
    {
        public Task<Unit> Handle(PostCreateUserCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
