using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PGP.Application.Exceptions;
using PGP.Application.Users.Commands.DeleteUser;
using PGP.Application.Users.Commands.CreateUser;
using PGP.Application.Users.Commands.UpdateUser;
using PGP.Application.Users.UserLogin;
using PGP.Application.Users.Queries.GetAllUsers;
using PGP.Application.Users.Queries.GetUserById;
using System.Threading.Tasks;
using PGP.Application.Users.Queries.GetAllUserCreatedPets;

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

        [HttpGet("{id}/pets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllUserCreatedPets(int id)
        {
            return Ok(await Mediator.Send(new GetAllUserCreatedPetsQuery { UserId = id }));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetUserByIdQuery { Id = id }));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Create([FromBody] CreateUserCommand command)
        {
            try
            {
                await Mediator.Send(command);
                return Ok();
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Login([FromBody] UserLoginCommand command)
        {
            try
            {
                return Ok(await Mediator.Send(command));
            }
            catch (UnauthorizedException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateUserCommand command)
        {
            try
            {
                if (command.Id == 0)
                {
                    command.Id = id;
                }

                await Mediator.Send(command);

                return Ok();
            }
            catch (UnauthorizedException ex) {
                return Unauthorized(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteUserCommand { Id = id } );

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}