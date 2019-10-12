using MediatR;

namespace PGP.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
