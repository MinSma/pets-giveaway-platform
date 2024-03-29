﻿using System;

namespace PGP.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public int CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }

        public int PetId { get; set; }
        public virtual Pet Pet { get; set; }
    }
}
