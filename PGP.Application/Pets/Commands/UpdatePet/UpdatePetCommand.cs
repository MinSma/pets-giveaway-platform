﻿using MediatR;
using PGP.Domain.Enums;

namespace PGP.Application.Pets.Commands.UpdatePet
{
    public class UpdatePetCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public Gender Gender { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public bool? IsSterilized { get; set; }
        public string Description { get; set; }
        public string PhotoCode { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}