using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResponse>
    {
        private readonly IPGPDbContext _context;

        public GetCategoryByIdQueryHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<GetCategoryByIdQueryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Categories
                .AsNoTracking()
                .Select(x => new GetCategoryByIdQueryResponse
                {
                    Id = x.Id,
                    Title = x.Title
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
