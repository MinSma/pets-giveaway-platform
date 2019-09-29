using MediatR;
using System.Collections.Generic;

namespace PGP.Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQuery : IRequest<List<GetAllCategoriesQueryResponse>>
    {
    }
}
