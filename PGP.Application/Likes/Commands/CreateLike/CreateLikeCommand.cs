using MediatR;

namespace PGP.Application.Likes.Commands.CreateLike
{
    public class CreateLikeCommand : IRequest
    {
        public int UserId { get; set; }
        public int PetId { get; set; }
    }
}
