﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Common.Interfaces;
using PGP.Application.Exceptions;
using PGP.Persistence;

namespace PGP.Application.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Unit>
    {
        private readonly IPGPDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public UpdateCommentCommandHandler(IPGPDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            if (_currentUserService.UserId != request.UserId && !_currentUserService.Role.Equals("Admin"))
            {
                throw new UnauthorizedException("You can't do this action.");
            }

            var comment = await _context.Comments
                .FirstOrDefaultAsync(x => x.Id == request.Id && x.CreatedByUserId == request.UserId, cancellationToken);

            if (comment == null)
            {
                throw new NotFoundException($"Comment id {request.Id} not exists.");
            }

            comment.Text = request.Text;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
