using MediatR;
using System.Collections.Generic;

namespace PGP.Application.Comments.Queries.GetAllComments
{
    public class GetAllCommentsQuery : IRequest<List<GetAllCommentsQueryResponse>>
    {
    }
}
