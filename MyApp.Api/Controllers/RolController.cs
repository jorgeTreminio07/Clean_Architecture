using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyApp.Application.Commands.Rol;
using MyApp.Application.Dtos.Rol;
using MyApp.Application.Queries;
using MyApp.Application.Queries.Offices;
using MyApp.Application.Queries.Rol;

namespace MyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllRolQuery());
            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound();
            }
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RolReqDto dto)
        {
            var command = new AddRolCommand(dto.Name, dto.Description, dto.NamesPermisos);
            var result = await _mediator.Send(command);
            if (result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }
            return Ok(result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateRolReqDto dto)
        {
            var command = new UpdateRolCommand(dto.Id,dto.Name, dto.Description, dto.NamesPermisos);
            var result = await _mediator.Send(command);
            if (result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }
            return Ok(result.Value);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var command = new DeleteRolCommand(id);
            var result = await _mediator.Send(command);
            if (result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }
            if (result.Status == ResultStatus.NotFound)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);
        }
    }
}
