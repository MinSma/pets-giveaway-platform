using MediatR;

namespace PGP.Application.Likes.Commands.DeleteLikeCommand
{
    public class DeleteLikeCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public int PetId { get; set; }
    }
}
