using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Exceptions;
using PGP.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
    {
        private readonly IPGPDbContext _context;

        public GetUserByIdQueryHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<GetUserByIdQueryResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Where(x => x.Id == request.Id)
                .Select(x => new GetUserByIdQueryResponse
                {
                    Id = x.Id,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    PhotoCode = x.PhotoCode
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new NotFoundException($"User id {request.Id} not exists.");
            }

            return user;
        }
    }
}
