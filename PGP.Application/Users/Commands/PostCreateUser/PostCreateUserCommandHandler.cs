﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Exceptions;
using PGP.Application.Helpers;
using PGP.Domain.Entities;
using PGP.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Users.Commands.PostCreateUser
{
    public class PostCreateUserCommandHandler : IRequestHandler<PostCreateUserCommand, Unit>
    {
        private readonly IPGPDbContext _context;

        public PostCreateUserCommandHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(PostCreateUserCommand request, CancellationToken cancellationToken)
        {
            var userExists = await _context.Users
                .AnyAsync(x => x.Email.ToLower().Equals(request.Email.ToLower()));

            if (userExists)
            {
                throw new ConflictException($"User with email: {request.Email} already exists.");
            }

            var user = new User
            {
                Password = AuthHelper.GetPasswordHash(request.Password),
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                RoleId = 1,
                PhotoCode = request.PhotoCode
            };

            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}