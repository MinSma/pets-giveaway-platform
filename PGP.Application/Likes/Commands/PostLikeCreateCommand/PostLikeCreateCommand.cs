using MediatR;

namespace PGP.Application.Likes.Commands.PostLikeCreateCommand
{
    public class PostLikeCreateCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public int PetId { get; set; }
    }
}
