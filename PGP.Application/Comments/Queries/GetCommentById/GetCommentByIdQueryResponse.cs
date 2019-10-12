using System;

namespace PGP.Application.Comments.Queries.GetCommentById
{
    public class GetCommentByIdQueryResponse
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CreatedByUserId { get; set; }
        public int PetId { get; set; }
    }
}
