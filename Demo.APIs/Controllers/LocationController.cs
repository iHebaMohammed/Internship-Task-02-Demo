using Demo.Application.Features.Location.Command;
using Demo.Application.Features.Location.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationController(IMediator mediator)
        {
            _mediator=mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            var query = new GetAllLocationsQuery();
            var result = await _mediator.Send(query);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("GetById{id:guid}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var query = new GetLocationByIdQuery(id);
            var result = await _mediator.Send(query);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult> Insert(InsertLocationCommand command)
        {
            var result = await _mediator.Send(command);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(UpdateLocationCommand command)
        {
            var result = await _mediator.Send(command);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteLocation(DeleteLocationCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
