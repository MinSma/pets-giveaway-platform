using MediatR;
using Microsoft.EntityFrameworkCore;
using PGP.Application.Exceptions;
using PGP.Domain.Entities;
using PGP.Persistence;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PGP.Application.Categories.Commands.PostCreateCategory
{
    public class PostCreateCategoryCommandHandler : IRequestHandler<PostCreateCategoryCommand, Unit>
    {
        private readonly IPGPDbContext _context;

        public PostCreateCategoryCommandHandler(IPGPDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(PostCreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var categoryWithNameExist = await _context.Categories
                .AnyAsync(x => x.Title.ToLower().Equals(request.Title.ToLower()));

            if (categoryWithNameExist)
            {
                throw new ConflictException($"Category with name {request.Title} already exists.");
            }

            await _context.Categories.AddAsync(new Category
            {
                Title = request.Title
            }, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}