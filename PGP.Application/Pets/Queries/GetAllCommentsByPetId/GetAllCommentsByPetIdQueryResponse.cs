﻿using PGP.Domain.Entities;
using System;

namespace PGP.Application.Pets.Queries.GetAllCommentsByPetId
{
    public class GetAllCommentsByPetIdQueryResponse
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public User CreatedByUser { get; set; }
    }
}