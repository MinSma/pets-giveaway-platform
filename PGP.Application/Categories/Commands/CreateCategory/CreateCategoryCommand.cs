using MediatR;

namespace PGP.Application.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest
    {
        public string Title { get; set; }
    }
}
