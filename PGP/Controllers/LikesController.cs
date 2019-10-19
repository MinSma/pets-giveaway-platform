using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PGP.Application.Exceptions;
using PGP.Application.Likes.Commands.DeleteLike;
using PGP.Application.Likes.Commands.CreateLike;
using PGP.Application.Likes.Queries.GetAllLikedPetsByUserId;
using System.Threading.Tasks;

namespace PGP.WebUI.Controllers
{
    [Authorize]
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Create(int userId, int petId)
        {
            try
            {
                await Mediator.Send(new CreateLikeCommand { UserId = userId, PetId = petId });

                return Ok();
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("pets/{petId}/likes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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