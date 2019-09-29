using MediatR;

namespace PGP.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<GetUserByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
