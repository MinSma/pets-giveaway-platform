using Microsoft.AspNetCore.Mvc;
using PGP.Application.Pets.Commands.DeletePet;
using PGP.Application.Pets.Commands.PostCreatePet;
using PGP.Application.Pets.Commands.PutUpdatePet;
using PGP.Application.Pets.Queries.GetAllPets;
using PGP.Application.Pets.Queries.GetPetById;
using System.Threading.Tasks;

namespace PGP.WebUI.Controllers
{
    [Route("api/[controller]")]
    public class PetsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetAll([FromQuery] GetAllPetsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromQuery] GetPetByIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] PostCreatePetCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] PutUpdatePetCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromQuery] DeletePetCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
