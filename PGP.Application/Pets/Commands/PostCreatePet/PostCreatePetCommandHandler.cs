using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Pets.Commands.PostCreatePet
{
    public class PostCreatePetCommandHandler : IRequestHandler<PostCreatePetCommand, Unit>
    {
        public Task<Unit> Handle(PostCreatePetCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
