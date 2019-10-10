﻿using MediatR;

namespace PGP.Application.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
