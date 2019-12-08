using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Common.Interfaces;
using PGP.Application.Models;
using PGP.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Users.Queries.GetAllUserCreatedPets
{
    public class GetAllUserCreatedPetsQueryHandler : IRequestHandler<GetAllUserCreatedPetsQuery, List<GetAllUserCreatedPetsQueryResponse>>
    {
        private readonly IPGPDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetAllUserCreatedPetsQueryHandler(IPGPDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<List<GetAllUserCreatedPetsQueryResponse>> Handle(GetAllUserCreatedPetsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Pets
                .Where(x => x.UserId == request.UserId)
                .Select(x => new GetAllUserCreatedPetsQueryResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    City = x.City,
                    Age = x.Age,
                    Gender = x.Gender,
                    State = x.State,
                    PhotoCode = x.PhotoCode,
                    Weight = x.Weight,
                    Height = x.Height,
                    IsSterilized = x.IsSterilized,
                    Description = x.Description,
                    DateAdded = x.DateAdded,
                    CategoryId = x.Category.Id
                })
                .ToListAsync(cancellationToken);
        }
    }
}
