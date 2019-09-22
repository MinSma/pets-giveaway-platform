using Microsoft.AspNetCore.Mvc;
using PGP.Application.Example;
using System.Threading.Tasks;

namespace PGP.WebUI.Controllers
{
    [Route("api/[controller]")]
    public class ExampleController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] GetAllQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
