﻿using MediatR;
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
                .Include(x => x.Photos)
                .Select(x => new GetAllPetsQueryResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    Gender = x.Gender,
                    Weight = x.Weight,
                    Height = x.Height,
                    IsSterilized = x.IsSterilized,
                    Description = x.Description,
                    DateAdded = x.DateAdded,
                    State = x.State,
                    MainPhotoUrl = x.Photos.FirstOrDefault(p => p.IsMain).Url
                })
                .ToListAsync(cancellationToken);
        }
    }
}