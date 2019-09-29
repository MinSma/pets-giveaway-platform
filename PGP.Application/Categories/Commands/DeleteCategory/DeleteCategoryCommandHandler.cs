using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        public Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
