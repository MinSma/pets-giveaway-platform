using MediatR;

namespace PGP.Application.Likes.Commands.CreateLike
{
    public class CreateLikeCommand : IRequest<Unit>
    {
        public int UserId { get; set; }
        public int PetId { get; set; }
    }
}
