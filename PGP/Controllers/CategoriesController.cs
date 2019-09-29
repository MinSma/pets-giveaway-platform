using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PGP.Application.Categories.Commands.DeleteCategory;
using PGP.Application.Categories.Commands.PostCreateCategory;
using PGP.Application.Categories.Commands.PutUpdateCategory;
using PGP.Application.Categories.Queries.GetAllCategories;
using PGP.Application.Categories.Queries.GetCategoryById;
using System.Threading.Tasks;

namespace PGP.WebUI.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAll([FromQuery] GetAllCategoriesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetById([FromQuery] GetCategoryByIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Create([FromBody] PostCreateCategoryCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromBody] PutUpdateCategoryCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromQuery] DeleteCategoryCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }
    }
}
