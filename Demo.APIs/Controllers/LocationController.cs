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

        [HttpGet("GetAllLocations")]
        public async Task<ActionResult> GetAllLocations()
        {
            var query = new GetAllLocationsQuery();
            var result = await _mediator.Send(query);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("GetLocationById{id:guid}")]
        public async Task<ActionResult> GetLocationById(Guid id)
        {
            var query = new GetLocationByIdQuery(id);
            var result = await _mediator.Send(query);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("InsertLocation")]
        public async Task<ActionResult> InsertLocation(InsertLocationCommand command)
        {
            var result = await _mediator.Send(command);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("UpdateLocation")]
        public async Task<ActionResult> UpdateLocation(UpdateLocationCommand command)
        {
            var result = await _mediator.Send(command);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("DeleteLocation")]
        public async Task<ActionResult> DeleteLocation(DeleteLocationCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
