using MediatR;

namespace PGP.Application.Pets.Commands.DeletePet
{
    public class DeletePetCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
