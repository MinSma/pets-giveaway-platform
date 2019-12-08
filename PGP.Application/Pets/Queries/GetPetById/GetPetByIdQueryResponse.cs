using PGP.Application.Comments;
using PGP.Application.Users;
using System;
using System.Collections.Generic;

namespace PGP.Application.Pets.Queries.GetPetById
{
    public class GetPetByIdQueryResponse : PetDto
    {
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public bool? IsSterilized { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public int CategoryId { get; set; }
        public UserDto CreatedByUser { get; set; }

        public ICollection<CommentDto> Comments { get; set; }
    }
}
