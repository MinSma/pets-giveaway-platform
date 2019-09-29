using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Categories.Commands.PostCreateCategory
{
    public class PostCreateCategoryCommandHandler : IRequestHandler<PostCreateCategoryCommand, Unit>
    {
        public Task<Unit> Handle(PostCreateCategoryCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
