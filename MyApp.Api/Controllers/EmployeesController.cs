using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MyApp.Application.Commands.Employee;
using MyApp.Application.Dtos.Employee;
using MyApp.Application.Queries.Employees;

namespace MyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IOutputCacheStore _outputCacheStore;

        private const string Cache = "obtener-employees";

        public EmployeesController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("{id:Guid}", Name = "GetEmployeeById")]
        [Authorize(Policy = "GetEmployeeById")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetEmployeeByIdQuery(id));

            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        [OutputCache(Tags = [Cache])]
        [Authorize(Policy = "GetEmployees")]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllEmpployeesQuery());
            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound();
            }
            return Ok(result.Value);
        }

        [HttpPost]
        [Authorize(Policy = "PostEmployee")]
        public async Task<IActionResult> Post([FromBody] EmployeeReqDto dto)
        {
            var result = await _mediator.Send(new AddEmployeeCommand(dto.Name, dto.Email, dto.Phone, dto.OfficeId));
            if (result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }

            await _outputCacheStore.EvictByTagAsync(Cache, default);
            return CreatedAtRoute("GetEmployeeById", new { id = result.Value.Id }, new
            {
                Employee = result.Value,
            });
        }

        [HttpPut]
        [Authorize(Policy = "UpdateEmployee")]
        public async Task<IActionResult> Put([FromBody] UpdateEmployeeReqDto dto)
        {
            var result = await _mediator.Send(new UpdateEmployeeCommand(dto.Id, dto.Name, dto.Email, dto.Phone, dto.OfficeId));
            if (result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }

            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(result.Errors); // muestra el error del catch en el CommandHandler
            }

            await _outputCacheStore.EvictByTagAsync(Cache, default);
            return Ok(result.Value);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "DeleteEmployee")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteEmployeeCommand(id));

            if (result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }

            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(result.Errors);
            }

            await _outputCacheStore.EvictByTagAsync(Cache, default);
            return Ok(result.Value);
        }

    }
}
