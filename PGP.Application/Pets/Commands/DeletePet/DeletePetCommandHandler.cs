using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Pets.Commands.DeletePet
{
    public class DeletePetCommandHandler : IRequestHandler<DeletePetCommand, Unit>
    {
        public Task<Unit> Handle(DeletePetCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
