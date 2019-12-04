using PGP.Application.Users;
using System;

namespace PGP.Application.Comments
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDto CreatedByUser { get; set; }
    }
}
