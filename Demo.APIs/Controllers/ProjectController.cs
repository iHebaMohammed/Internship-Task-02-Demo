using Demo.Application.Features.Project.Command;
using Demo.Application.Features.Project.Ouery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator=mediator;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAllProjects()
        {
            var query = new GetAllProjectsQuery();
            var result = await _mediator.Send(query);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("GetById{id:guid}")]
        public async Task<ActionResult> GetProjectById(Guid id)
        {
            var query = new GetProjectByIdQuery(id);
            var result = await _mediator.Send(query);
            if(result.IsSuccess) 
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPost("Insert")]
        public async Task<ActionResult> Insert(InsertProjectCommand command)
        {
            var result = await _mediator.Send(command);
            if(result.IsSuccess) 
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update(UpdateProjectCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(DeleteProjectCommand command)
        {
            var result = await _mediator.Send(command);
            if(result.IsSuccess)
                return Ok(result);
            return BadRequest(result);
        }

    }
}
