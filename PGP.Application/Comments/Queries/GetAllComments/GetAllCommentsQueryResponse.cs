using System;

namespace PGP.Application.Comments.Queries.GetAllComments
{
    public class GetAllCommentsQueryResponse
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
    }
}
