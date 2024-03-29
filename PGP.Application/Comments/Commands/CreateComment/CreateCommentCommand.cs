﻿using MediatR;

namespace PGP.Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<CreateCommentCommandResponse>
    {
        public int PetId { get; set; }
        public int UserId { get; set; }
        public string Text { get; set; }

    }
}
