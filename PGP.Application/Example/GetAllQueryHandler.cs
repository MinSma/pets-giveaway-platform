using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace PGP.Application.Example
{
    public class GetAllQueryHandler : IRequestHandler<GetAllQuery, bool>
    {
        public Task<bool> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }
    }
}
