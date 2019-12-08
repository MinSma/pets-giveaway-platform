using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Common.Interfaces;
using PGP.Application.Exceptions;
using PGP.Application.Users;
using PGP.Domain.Entities;
using PGP.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CreateCommentCommandResponse>
    {
        private readonly IPGPDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public CreateCommentCommandHandler(IPGPDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<CreateCommentCommandResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _context.Users.AnyAsync(x => x.Id == request.UserId);

            if (!userExists)
            {
                throw new NotFoundException($"User was not found with specified id = {request.UserId}");
            }

            if (_currentUserService.UserId != request.UserId)
            {
                throw new UnauthorizedException("You can't do this action.");
            }

            var petExists = await _context.Pets.AnyAsync(x => x.Id == request.PetId);

            if (!petExists)
            {
                throw new NotFoundException($"Pet with specified id = {request.PetId} not exist.");
            }

            var comment = new Comment
            {
                Text = request.Text,
                PetId = request.PetId,
                CreatedAt = DateTime.Now,
                CreatedByUserId = request.UserId
            };

            await _context.Comments.AddAsync(comment, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var user = await _context.Users.FindAsync(comment.CreatedByUserId);

            return new CreateCommentCommandResponse
            {
                Id = comment.Id,
                CreatedAt = comment.CreatedAt,
                Text = comment.Text,
                CreatedByUser = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    RoleId = user.RoleId
                }
            };
        }
    }
}
