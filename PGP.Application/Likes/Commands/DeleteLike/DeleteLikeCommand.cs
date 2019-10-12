using MediatR;

namespace PGP.Application.Likes.Commands.DeleteLike
{
    public class DeleteLikeCommand : IRequest
    {
        public int UserId { get; set; }
        public int PetId { get; set; }
    }
}
