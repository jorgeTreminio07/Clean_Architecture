using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands.Clase;
using MyApp.Application.Dtos.Clase;
using MyApp.Application.Queries.Clase;

namespace MyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClaseController(IMediator mediator)
        {
            this._mediator = mediator;
        }


        [HttpGet("{id:Guid}", Name = "GetClaseById")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetClaseByIdQuery(id));
            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(result.Errors);
            }
            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllClasesQuery());
            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(result.Errors);
            }
            return Ok(result.Value);

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddClaseReqDto dto)
        {
            var result = await _mediator.Send(new AddClaseCommand(dto.Name, dto.EstudiantesId));
            if(result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }
            return CreatedAtRoute("GetClaseById", new { Id = result.Value.Id}, new
            { 
                Clase = result.Value,
            });
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateClaseReqDto dto)
        {
            var result = await _mediator.Send(new UpdateClaseCommand(dto.Id,dto.Name, dto.EstudiantesId));
            if (result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);

            }
            return Ok(result.Value);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteClaseCommand(id));
            if(result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }

            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(result.Errors);
            }
            return Ok(result.Value);
        }
    }
}
