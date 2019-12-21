using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<GetAllCategoriesByUserIdQueryResponse>>
    {
        private readonly IPGPDbContext _context;

        public GetAllCategoriesQueryHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllCategoriesByUserIdQueryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .Select(x => new GetAllCategoriesByUserIdQueryResponse
                {
                    Id = x.Id,
                    Text = x.Title
                })
                .ToListAsync(cancellationToken);
        }
    }
}
