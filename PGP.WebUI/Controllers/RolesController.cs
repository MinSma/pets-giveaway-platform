using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGP.Application.Roles.Queries.GetRoles;
using System.Threading.Tasks;

namespace PGP.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class RolesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetAllRoles()
        {
            return Ok(await Mediator.Send(new GetAllRolesQuery()));
        }
    }
}
