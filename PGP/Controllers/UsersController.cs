using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PGP.Application.Users.Commands.DeleteUser;
using PGP.Application.Users.Commands.PostCreateUser;
using PGP.Application.Users.Commands.PutUpdateUser;
using PGP.Application.Users.PostUserLogin;
using PGP.Application.Users.Queries.GetAllUsers;
using PGP.Application.Users.Queries.GetUserById;
using System;
using System.Threading.Tasks;

namespace PGP.WebUI.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll([FromQuery] GetAllUsersQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById([FromQuery] GetUserByIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Create([FromBody] PostCreateUserCommand command)
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Login([FromBody] PostUserLoginCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch(InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromBody] PutUpdateUserCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromQuery] DeleteUserCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }
    }
}