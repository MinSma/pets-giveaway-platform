using MediatR;

namespace PGP.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Unit>
    {
        public string Title { get; set; }
    }
}
