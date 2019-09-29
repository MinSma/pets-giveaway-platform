using MediatR;

namespace PGP.Application.Comments.Commands.PostCreateComment
{
    public class PostCreateCommentCommand : IRequest<Unit>
    {
        public int PetId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }

    }
}
