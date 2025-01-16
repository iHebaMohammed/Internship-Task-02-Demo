using Demo.Application.Features.Location.Query;
using Demo.Application.Features.Project.Ouery;
using Demo.Domain.Common;
using Demo.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Features.Project.Command
{

    public record UpdateProjectCommand(Guid Id, string Name , DateTime startDate) : IRequest<UpdateProjectCommandResult>
    {
    }
    public record UpdateProjectCommandResult : BaseCommonResult
    {
        public ProjectVM Project { get; set; }
    }

    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, UpdateProjectCommandResult>
    {
        private readonly ApplicationDbContext _context;

        public UpdateProjectCommandHandler(ApplicationDbContext context)
        {
            this._context=context;
        }
        public async Task<UpdateProjectCommandResult> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var project = await _context.Projects.Where(project => project.Id == request.Id).FirstOrDefaultAsync();

                if (project != null)
                {
                    project.Name = request.Name;
                    project.UpdatedAt = DateTime.Now;
                    project.StartDate = request.startDate;

                    _context.Projects.Update(project);
                    await _context.SaveChangesAsync();

                    return new UpdateProjectCommandResult()
                    {
                        IsSuccess = true,
                        Project = new ProjectVM
                        {
                            Id = request.Id,
                            Name = request.Name,
                            StartDate = request.startDate
                        }
                    };
                }

                return new UpdateProjectCommandResult()
                {
                    IsSuccess = false,
                    Errors = { $"This id {request.Id} Not Found" },
                    StatusCode = StatusCode.NotFound

                };
            }
            catch (Exception ex)
            {
                return new UpdateProjectCommandResult
                {
                    IsSuccess = false,
                    Errors = { ex.Message },
                    StatusCode = StatusCode.Error
                };
            }
        }
    }
}
