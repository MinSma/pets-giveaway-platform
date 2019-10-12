using MediatR;

namespace PGP.Application.Comments.Queries.GetCommentById
{
    public class GetCommentByIdQuery : IRequest<GetCommentByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
