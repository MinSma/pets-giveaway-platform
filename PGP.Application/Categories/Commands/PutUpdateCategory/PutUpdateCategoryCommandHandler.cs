using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Exceptions;
using PGP.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Categories.Commands.PutUpdateCategory
{
    public class PutUpdateCategoryCommandHandler : IRequestHandler<PutUpdateCategoryCommand, Unit>
    {
        private readonly IPGPDbContext _context;

        public PutUpdateCategoryCommandHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(PutUpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (category == null)
            {
                throw new NotFoundException($"Category id {request.Id} not exists.");
            }

            category.Title = request.Title;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
