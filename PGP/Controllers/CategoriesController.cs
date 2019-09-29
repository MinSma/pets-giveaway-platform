using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PGP.Application.Categories.Commands.DeleteCategory;
using PGP.Application.Categories.Commands.PostCreateCategory;
using PGP.Application.Categories.Commands.PutUpdateCategory;
using PGP.Application.Categories.Queries.GetAllCategories;
using PGP.Application.Categories.Queries.GetCategoryById;
using PGP.Application.Exceptions;
using System.Threading.Tasks;

namespace PGP.WebUI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class CategoriesController : BaseController
    {
        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetAllCategoriesQuery()));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                return Ok(await Mediator.Send(new GetCategoryByIdQuery { Id = id }));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Create([FromBody] PostCreateCategoryCommand command)
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

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromBody] PutUpdateCategoryCommand command)
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteCategoryCommand { Id = id });

                return Ok();
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
