using MediatR;

namespace PGP.Application.Categories.Commands.PutUpdateCategory
{
    public class PutUpdateCategoryCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
