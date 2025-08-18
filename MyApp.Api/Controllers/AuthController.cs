using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MyApp.Application.Commands.Rol;
using MyApp.Application.Commands.User;
using MyApp.Application.Dtos.User;
using MyApp.Application.Queries.User;

namespace MyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private const string cache = "obtener-usuarios";
        private readonly IOutputCacheStore _outputCacheStore;

        public AuthController(IMediator mediator, IOutputCacheStore outputCacheStore)
        {
            this._mediator = mediator;
            this._outputCacheStore = outputCacheStore;
        }

        [HttpGet("{id:Guid}", Name ="GetUserById")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var command = new GetUserByIdQuery(id);
            var result = await _mediator.Send(command);
            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        [OutputCache(Tags = [cache])]
        public async Task<IActionResult> Get()
        {
            var command = new GetAllUsersQuery();
            var result = await _mediator.Send(command);
            if(result.Status == ResultStatus.NotFound)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Post([FromForm] AddUserReqDto dto)
        {
            var command = new AddUserCommand(dto.Name, dto.Email, dto.Password, dto.RolId, dto.Avatar);
            var result = await _mediator.Send(command);
            
            if (result.Status == ResultStatus.Invalid)
            {
                return NotFound(result.ValidationErrors);
            }

            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(result.Errors);
            }

            await _outputCacheStore.EvictByTagAsync(cache, default);//actualizar cache
            return CreatedAtRoute("GetUserById", new { Id = result.Value.Id }, new
            {
                User = result.Value,
            });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginReqDto dto)
        {
            var command = new LoginUserCommand(dto.Email, dto.Password);
            var result = await _mediator.Send(command);

            if (result.Status == ResultStatus.Invalid)
            {
                return NotFound(result.ValidationErrors);
            }

            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromForm] UpdateUserReqDto dto)
        {
            var command = new UpdateUserCommand(dto.Id,dto.Name, dto.Email, dto.Password, dto.RolId, dto.Avatar);
            var result = await _mediator.Send(command);

            if (result.Status == ResultStatus.Invalid)
            {
                return NotFound(result.ValidationErrors);
            }

            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(result.Errors);
            }

            await _outputCacheStore.EvictByTagAsync(cache, default);//actualizar cache

            return Ok(result.Value);
        }

        

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var command = new DeleteUserCommand(id);
            var result = await _mediator.Send(command);
            if (result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }
            if (result.Status == ResultStatus.NotFound)
            {
                return BadRequest(result.Errors);
            }
            await _outputCacheStore.EvictByTagAsync(cache, default);//actualizar cache

            return Ok(result.Value);
        }

    }
}
