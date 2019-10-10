using MediatR;

namespace PGP.Application.Pets.Commands.DeletePet
{
    public class DeletePetCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Role { get; set; }
    }
}
