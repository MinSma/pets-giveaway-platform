using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Exceptions;
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
            var category = await _context.Categories
                .AsNoTracking()
                .Where(x => x.Id == request.Id)
                .Select(x => new GetCategoryByIdQueryResponse
                {
                    Id = x.Id,
                    Title = x.Title
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (category == null)
            {
                throw new NotFoundException($"Category id {request.Id} not exists.");
            }

            return category;
        }
    }
}
