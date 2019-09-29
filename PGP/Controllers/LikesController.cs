using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PGP.Application.Exceptions;
using PGP.Application.Likes.Commands.DeleteLikeCommand;
using PGP.Application.Likes.Commands.PostLikeCreateCommand;
using PGP.Application.Likes.Queries.GetAllLikedPetsByUserId;
using System.Threading.Tasks;

namespace PGP.WebUI.Controllers
{
    [Route("api/users/{userId}/")]
    public class LikesController : BaseController
    {
        [HttpGet("likes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllLikedPetsByUserId(int userId)
        {
            return Ok(await Mediator.Send(new GetAllLikedPetsByUserIdQuery { UserId = userId }));
        }

        [HttpPost("pets/{petId}/likes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> Create(int userId, int petId)
        {
            try
            {
                await Mediator.Send(new PostLikeCreateCommand { UserId = userId, PetId = petId });

                return Ok();
            }
            catch (ConflictException ex)
            {
                return Conflict(ex);
            }
        }

        [HttpDelete("pets/{petId}/likes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int userId, int petId)
        {
            try
            {
                await Mediator.Send(new DeleteLikeCommand { UserId = userId, PetId = petId });

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
