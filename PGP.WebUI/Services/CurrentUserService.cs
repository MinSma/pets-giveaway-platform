﻿using Microsoft.AspNetCore.Http;
using PGP.Application.Common.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace PGP.WebUI.Services
{
    [ExcludeFromCodeCoverage]
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = int.Parse(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier));
            Role = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
        }

        public int UserId { get; }
        public string Role { get; set; }
    }
}