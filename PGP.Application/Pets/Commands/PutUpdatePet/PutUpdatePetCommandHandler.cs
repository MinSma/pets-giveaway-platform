using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace PGP.Application.Pets.Commands.PutUpdatePet
{
    public class PutUpdatePetCommandHandler : IRequestHandler<PutUpdatePetCommand, Unit>
    {
        public Task<Unit> Handle(PutUpdatePetCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
