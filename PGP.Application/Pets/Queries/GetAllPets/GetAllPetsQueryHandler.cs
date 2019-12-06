using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Pets.Queries.GetAllPets
{
    public class GetAllPetsQueryHandler : IRequestHandler<GetAllPetsQuery, List<GetAllPetsQueryResponse>>
    {
        private readonly IPGPDbContext _context;

        public GetAllPetsQueryHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllPetsQueryResponse>> Handle(GetAllPetsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Pets
                .AsNoTracking()
                .Select(x => new GetAllPetsQueryResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    City = x.City,
                    Gender = x.Gender,
                    State = x.State,
                    PhotoCode = x.PhotoCode
                })
                .ToListAsync(cancellationToken);
        }
    }
}