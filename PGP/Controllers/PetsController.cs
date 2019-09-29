﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PGP.Application.Exceptions;
using PGP.Application.Pets.Commands.DeletePet;
using PGP.Application.Pets.Commands.PostCreatePet;
using PGP.Application.Pets.Commands.PutUpdatePet;
using PGP.Application.Pets.Queries.GetAllCommentsByPetId;
using PGP.Application.Pets.Queries.GetAllPets;
using PGP.Application.Pets.Queries.GetPetById;
using System.Threading.Tasks;

namespace PGP.WebUI.Controllers
{
    [Authorize]
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
        [HttpGet("{id}/comments")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetAllCommentsByPetId(int id)
        {
            return Ok(await Mediator.Send(new GetAllCommentsByPetIdQuery { Id = id }));
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
        public async Task<ActionResult> Create([FromBody] PostCreatePetCommand command)
        {
            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update([FromBody] PutUpdatePetCommand command)
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeletePetCommand { Id = id });

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
