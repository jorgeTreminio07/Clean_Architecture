using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
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
        private const string cache = "obtener-Roles";
        private readonly IOutputCacheStore _outputCacheStore;

        public RolController(IMediator mediator, IOutputCacheStore outputCacheStore)
        {
            this._mediator = mediator;
            this._outputCacheStore = outputCacheStore;
        }

        [HttpGet]
        [OutputCache(Tags = [cache])]
        [Authorize(Policy = "GetRol")]
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
        [Authorize(Policy = "PostRol")]
        public async Task<IActionResult> Post([FromBody] RolReqDto dto)
        {
            var command = new AddRolCommand(dto.Name, dto.Description, dto.NamesPermisos);
            var result = await _mediator.Send(command);
            if (result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }
            await _outputCacheStore.EvictByTagAsync(cache, default);
            return Ok(result.Value);
        }

        [HttpPut]
        [Authorize(Policy = "UpdateRol")]
        public async Task<IActionResult> Put([FromBody] UpdateRolReqDto dto)
        {
            var command = new UpdateRolCommand(dto.Id, dto.Name, dto.Description, dto.NamesPermisos);
            var result = await _mediator.Send(command);
            if (result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }
            await _outputCacheStore.EvictByTagAsync(cache, default);
            return Ok(result.Value);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "DeleteRol")]
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
            await _outputCacheStore.EvictByTagAsync(cache, default);
            return Ok(result.Value);
        }
    }
}
