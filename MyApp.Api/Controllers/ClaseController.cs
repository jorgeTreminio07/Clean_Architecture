using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
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

        private readonly IOutputCacheStore _outputCacheStore;

        private const string cache = "obtener-clases";

        public ClaseController(IMediator mediator, IOutputCacheStore outputCacheStore)
        {
            this._mediator = mediator;
            this._outputCacheStore = outputCacheStore;
        }


        [HttpGet("{id:Guid}", Name = "GetClaseById")]
        [Authorize(Policy = "GetClaseById")]
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
        [OutputCache(Tags = [cache])]
        [Authorize(Policy = "GetClase")]
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
        [Authorize(Policy = "PostClase")]
        public async Task<IActionResult> Post([FromBody] AddClaseReqDto dto)
        {
            var result = await _mediator.Send(new AddClaseCommand(dto.Name, dto.EstudiantesId));
            if (result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }

            await _outputCacheStore.EvictByTagAsync(cache, default);
            return CreatedAtRoute("GetClaseById", new { Id = result.Value.Id }, new
            {
                Clase = result.Value,
            });
        }

        [HttpPut]
        [Authorize(Policy = "UpdateClase")]
        public async Task<IActionResult> Put([FromBody] UpdateClaseReqDto dto)
        {
            var result = await _mediator.Send(new UpdateClaseCommand(dto.Id, dto.Name, dto.EstudiantesId));
            if (result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);

            }

            await _outputCacheStore.EvictByTagAsync(cache, default);
            return Ok(result.Value);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "DeleteClase")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteClaseCommand(id));
            if (result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }

            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(result.Errors);
            }
            await _outputCacheStore.EvictByTagAsync(cache, default);
            return Ok(result.Value);
        }
    }
}
