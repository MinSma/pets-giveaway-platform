using Microsoft.AspNetCore.Http;
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
            var nameIdentifier = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);

            if (nameIdentifier != null && role != null)
            {
                UserId = int.Parse(nameIdentifier);
                Role = role;
            }
        }

        public int UserId { get; }
        public string Role { get; set; }
    }
}
