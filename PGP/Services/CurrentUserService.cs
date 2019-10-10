using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace PGP.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            Role = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);
        }

        public string UserId { get; }
        public string Role { get; set; }
    }
}
