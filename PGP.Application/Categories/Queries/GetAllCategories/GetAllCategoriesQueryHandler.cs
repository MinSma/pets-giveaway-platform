using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<GetAllCategoriesQueryResponse>>
    {
        private readonly IPGPDbContext _context;

        public GetAllCategoriesQueryHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllCategoriesQueryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .AsNoTracking()
                .Select(x => new GetAllCategoriesQueryResponse
                {
                    Id = x.Id,
                    Title = x.Title
                })
                .ToListAsync(cancellationToken);
        }
    }
}
