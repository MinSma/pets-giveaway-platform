using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Categories.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<GetAllCategoriesQueryResponse>>
    {
        public Task<List<GetAllCategoriesQueryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
