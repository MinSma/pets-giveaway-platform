using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Categories.Commands.PutUpdateCategory
{
    public class PutUpdateCategoryCommandHandler : IRequestHandler<PutUpdateCategoryCommand, Unit>
    {
        public Task<Unit> Handle(PutUpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
