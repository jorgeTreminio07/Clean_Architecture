using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Commands.Estudiante;
using MyApp.Application.Dtos.Estudiante;
using MyApp.Application.Queries.Estudiante;

namespace MyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudianteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EstudianteController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("{id:Guid}",Name = "GetEstudianteById")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var result = await _mediator.Send(new GetEstudianteByIdQuery(id));
            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllEstudianteQuery());
            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Value);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddEstudianteReqDto dto)
        {
            var result = await _mediator.Send(new AddEstudianteCommand(dto.Name, dto.ClasesId));
            if (result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }
            return CreatedAtRoute("GetEstudianteById", new { Id = result.Value.Id }, new 
            {
                Estudiante = result.Value,
            });
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateEstudianteReqDto dto)
        {
            var result = await _mediator.Send(new UpdateEstudianteComand(dto.Id, dto.Name, dto.ClasesId));
            if (result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }
            return Ok(new { message = "Estudiante Editado correctamente", result.Value });
        }


        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteEstudianteCommand(id));
            if(result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }
            if (result.Status == ResultStatus.NotFound)
            {
                return BadRequest(result.Errors);
            }
            return Ok(new { message = "Estudiante eliminado correctamente", result.Value });
        }
    }
}
