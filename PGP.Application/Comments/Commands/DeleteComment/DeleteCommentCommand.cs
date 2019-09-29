using MediatR;

namespace PGP.Application.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
