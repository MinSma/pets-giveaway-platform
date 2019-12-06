using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Comments;
using PGP.Application.Exceptions;
using PGP.Application.Users;
using PGP.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Pets.Queries.GetPetById
{
    public class GetPetByIdQueryHandler : IRequestHandler<GetPetByIdQuery, GetPetByIdQueryResponse>
    {
        private readonly IPGPDbContext _context;

        public GetPetByIdQueryHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<GetPetByIdQueryResponse> Handle(GetPetByIdQuery request, CancellationToken cancellationToken)
        {
            var pet = await _context.Pets
                .Include(x => x.Comments)
                .Where(x => x.Id == request.Id)
                .Select(x => new GetPetByIdQueryResponse
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    Gender = x.Gender,
                    Weight = x.Weight,
                    Height = x.Height,
                    IsSterilized = x.IsSterilized,
                    Description = x.Description,
                    DateAdded = x.DateAdded,
                    PhotoCode = x.PhotoCode,
                    State = x.State,
                    Comments = x.Comments.Select(c => new CommentDto
                    {
                        Id = c.Id,
                        Text = c.Text,
                        CreatedAt = c.CreatedAt,
                        CreatedByUser = new UserDto
                        {
                            Id = c.CreatedByUser.Id,
                            Email = c.CreatedByUser.Email,
                            PhoneNumber = c.CreatedByUser.PhoneNumber,
                            FirstName = c.CreatedByUser.FirstName,
                            LastName = c.CreatedByUser.LastName
                        }
                    }).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (pet == null)
            {
                throw new NotFoundException($"Pet id {request.Id} not exists.");
            }

            return pet;
        }
    }
}
