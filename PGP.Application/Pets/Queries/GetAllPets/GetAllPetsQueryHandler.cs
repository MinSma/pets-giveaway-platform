using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Common.Interfaces;
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
        private readonly ICurrentUserService _currentUserService;

        public GetAllPetsQueryHandler(IPGPDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<List<GetAllPetsQueryResponse>> Handle(GetAllPetsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Pets
                .Select(x => new GetAllPetsQueryResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    City = x.City,
                    Gender = x.Gender,
                    State = x.State,
                    PhotoCode = x.PhotoCode,
                    IsLiked = _currentUserService.UserId > 0 ? x.Likes.Any(l => l.UserId == _currentUserService.UserId) : false
                })
                .ToListAsync(cancellationToken);
        }
    }
}