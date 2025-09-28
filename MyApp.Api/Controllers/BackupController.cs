using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MyApp.Application.Commands.Backups;
using MyApp.Application.Commands.Clase;
using MyApp.Application.Queries.Backups;

namespace MyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IOutputCacheStore _outputCacheStore;

        private const string cache = "obtener-backups";

        public BackupController(IMediator mediator, IOutputCacheStore outputCacheStore)
        {
            this._mediator = mediator;
            this._outputCacheStore = outputCacheStore;
        }

        [HttpGet]
        [OutputCache(Tags = [cache])]
        [Authorize(Policy = "GetBackup")]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllBackupsQuery();
            var result = await _mediator.Send(query);

            if (result.Status == ResultStatus.NotFound)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);

        }


        [HttpPost]
        [Authorize(Policy = "PostBackup")]
        public async Task<IActionResult> Post()
        {
            var result = await _mediator.Send(new AddBackupCommand());
            if (result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }

            await _outputCacheStore.EvictByTagAsync(cache, default);
            return Ok(result.Value);
            //return CreatedAtRoute("GetClaseById", new { Id = result.Value.Id }, new
            //{
            //    Clase = result.Value,
            //});
        }

        [HttpPost("{id:Guid}")]
        [Authorize(Policy = "RestoreBackup")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new RestoreBackupCommand(id));
            if (result.Status == ResultStatus.NotFound)
            {
                return BadRequest(result.Errors);
            }
            if (result.Status == ResultStatus.Error)
            {
                return BadRequest(result.Errors);
            }

            await _outputCacheStore.EvictByTagAsync(cache, default);
            return Ok(result.Value);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "DeleteBackup")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteBackupCommand(id));
            if (result.Status == ResultStatus.NotFound)
            {
                return BadRequest(result.Errors);
            }
            if (result.Status == ResultStatus.Error)
            {
                return BadRequest(result.Errors);
            }

            await _outputCacheStore.EvictByTagAsync(cache, default);
            return Ok(result.Value);
        }
    }
}
