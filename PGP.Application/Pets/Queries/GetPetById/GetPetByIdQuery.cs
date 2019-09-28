using MediatR;

namespace PGP.Application.Pets.Queries.GetPetById
{
    public class GetPetByIdQuery : IRequest<GetPetByIdQueryResponse>
    {
        public int Id { get; set; }
    }
}
