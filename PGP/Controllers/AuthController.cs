using Microsoft.AspNetCore.Mvc;
using PGP.Application.Auth.PostUserLogin;
using PGP.Application.Auth.PostUserRegistration;
using System;
using System.Threading.Tasks;

namespace PGP.WebUI.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : BaseController
    {
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] PostUserRegistrationCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] PostUserLoginCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
