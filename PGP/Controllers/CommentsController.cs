using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PGP.Application.Comments.Commands.DeleteComment;
using PGP.Application.Comments.Commands.CreateComment;
using PGP.Application.Comments.Commands.UpdateComment;
using PGP.Application.Exceptions;
using System.Threading.Tasks;

namespace PGP.WebUI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CommentsController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCommentCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] UpdateCommentCommand command)
        {
            try
            {
                await Mediator.Send(command);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteCommentCommand { Id = id });

                return Ok();
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
