﻿using MediatR;
using PGP.Domain.Enums;
using System;

namespace PGP.Application.Pets.Commands.CreatePet
{
    public class CreatePetCommand : IRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string City { get; set; }
        public Gender Gender { get; set; }
        public double? Weight { get; set; }
        public double? Height { get; set; }
        public bool? IsSterilized { get; set; }
        public string Description { get; set; }
        public string PhotoCode { get; set; }
        public int CategoryId { get; set; }
    }
}
