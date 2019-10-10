using MediatR;

namespace PGP.Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
