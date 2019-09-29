﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Likes.Queries.GetAllLikedPetsByUserId
{
    public class GetAllLikedPetsByUserIdQueryHandler : IRequestHandler<GetAllLikedPetsByUserIdQuery, List<GetAllLikedPetsByUserIdQueryResponse>>
    {
        private readonly IPGPDbContext _context;

        public GetAllLikedPetsByUserIdQueryHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllLikedPetsByUserIdQueryResponse>> Handle(GetAllLikedPetsByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Likes
                .Include(x => x.Pet)
                .Include(x => x.Pet.Photos)
                .Where(x => x.UserId == request.UserId)
                .Select(x => new GetAllLikedPetsByUserIdQueryResponse
                {
                    Name = x.Pet.Name,
                    Age = x.Pet.Age,
                    Gender = x.Pet.Gender,
                    Weight = x.Pet.Weight,
                    Height = x.Pet.Height,
                    IsSterilized = x.Pet.IsSterilized,
                    DateAdded = x.Pet.DateAdded,
                    State = x.Pet.State,
                    MainPhotoUrl = x.Pet.Photos.FirstOrDefault(p => p.IsMain).Url
                })
                .ToListAsync(cancellationToken);
        }
    }
}