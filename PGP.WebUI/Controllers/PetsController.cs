using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PGP.Application.Exceptions;
using PGP.Application.Pets.Commands.CreatePet;
using PGP.Application.Pets.Commands.DeletePet;
using PGP.Application.Pets.Commands.UpdatePet;
using PGP.Application.Pets.Queries.GetAllPets;
using PGP.Application.Pets.Queries.GetPetById;
using System.Threading.Tasks;

namespace PGP.WebUI.Controllers
{
    [Authorize(Roles = "Admin, Moderator")]
    [Route("api/[controller]")]
    public class PetsController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllPetsQuery()));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetPetByIdQuery { Id = id }));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Create([FromBody] CreatePetCommand command)
        {
            try
            {
                await Mediator.Send(command);

                return Ok();
            }
            catch (UnauthorizedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdatePetCommand command)
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
            catch (UnauthorizedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeletePetCommand { Id = id });

                return Ok();
            }
            catch (UnauthorizedException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
