using Demo.Application.Features.Kpi.Command;
using Demo.Application.Features.Kpi.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KpiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KpiController(IMediator mediator)
        {
            this._mediator=mediator;
        }

        [HttpGet("GetAllKpis")]
        public async Task<ActionResult> GetAll()
        {
            var query = new GetAllKpisQuery();
            var result = await _mediator.Send(query);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("GetKpiById/{id:guid}")]
        public async Task<ActionResult> GetById(Guid id) 
        {
            var query = new GetKpiByIdQuery(id);
            var result = await _mediator.Send(query);
            if(result.IsSuccess) 
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("InsertKpi")]
        public async Task<ActionResult> AddKpi(InsertKpiCommand command)
        {
            var result = await _mediator.Send(command);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("UpdateKpi")]
        public async Task<ActionResult> UpdateKpi(UpdateKpiCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("DeleteKpi")]
        public async Task<ActionResult> DeleteKpi(DeleteKpiCommand command)
        {
            var result = await _mediator.Send(command);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
