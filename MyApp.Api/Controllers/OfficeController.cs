using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Queries.Offices;
using Ardalis.Result;
using MyApp.Application.Dtos.Office;
using MyApp.Application.Commands.Office;
using Microsoft.AspNetCore.Authorization;

namespace MyApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfficeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OfficeController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("{id:Guid}", Name = "GetOfficeById")]
        [Authorize(Policy = "GetOfficeById")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetOfficeByIdQuery(id));
            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpGet]
        [Authorize(Policy = "GetOffice")]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllOfficesQuery());
            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound();
            }
            return Ok(result.Value);
        }

        [HttpPost]
        [Authorize(Policy = "PostOffice")]
        public async Task<IActionResult> Post([FromBody] AddOfficeReqDto dto)
        {
            var result = await _mediator.Send(new AddOfficeCommand(dto.Name));

            if (result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }

            return CreatedAtRoute("GetOfficeById", new { Id = result.Value.Id }, new
            {
                Office = result.Value,
            });
        }

        [HttpPut]
        [Authorize(Policy = "UpdateOffice")]
        public async Task<IActionResult> Put([FromBody] UpdateOfficeReqDto dto)
        {
            var result = await _mediator.Send(new UpdateOfficeCommand(dto.Id, dto.Name));

            if (result.Status == ResultStatus.Invalid)
            {
                return BadRequest(result.ValidationErrors);
            }

            if (result.Status == ResultStatus.NotFound)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Value);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Policy = "DeleteOffice")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteOfficeCommand(id));

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
