﻿using MediatR;
using PGP.Domain.Entities;
using PGP.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Comments.Commands.PostCreateComment
{
    public class PostCreateCommentCommandHandler : IRequestHandler<PostCreateCommentCommand, Unit>
    {
        private readonly IPGPDbContext _context;

        public PostCreateCommentCommandHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(PostCreateCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Comment
            {
                Text = request.Text,
                PetId = request.PetId,
                CreatedByUserId = request.UserId
            };

            await _context.Comments.AddAsync(comment, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
