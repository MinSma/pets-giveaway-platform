﻿namespace PGP.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        string Role { get; }
    }
}