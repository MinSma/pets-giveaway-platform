using MediatR;

namespace PGP.Application.Comments.Commands.PutUpdateComment
{
    public class PutUpdateCommentCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
