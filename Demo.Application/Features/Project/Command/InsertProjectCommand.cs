using Demo.Application.Features.Location.Query;
using Demo.Application.Features.Project.Ouery;
using Demo.Domain.Common;
using Demo.Infrastructure.Contexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Features.Project.Command
{
    public record InsertProjectCommand(string Name , DateTime StartDate) : IRequest<InsertProjectCommandResult>
    {
    }
    public record InsertProjectCommandResult : BaseCommonResult
    {
        public ProjectVM Project { get; set; }
    }

    public class InsertProjectCommandHandler : IRequestHandler<InsertProjectCommand, InsertProjectCommandResult>
    {
        private readonly ApplicationDbContext _context;

        public InsertProjectCommandHandler(ApplicationDbContext context)
        {
            this._context=context;
        }
        public async Task<InsertProjectCommandResult> Handle(InsertProjectCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var project = new Domain.Entities.Project
                {
                    CreatedAt = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    StartDate = request.StartDate,
                    UpdatedAt = DateTime.UtcNow,
                };
                await _context.Projects.AddAsync(project);
                await _context.SaveChangesAsync();
                return new InsertProjectCommandResult
                {
                    IsSuccess = true,
                    Project =  new ProjectVM
                    {
                        Id = project.Id,
                        Name = project.Name,
                        StartDate = project.StartDate
                    },
                };
            }
            catch (Exception ex)
            {
                return new InsertProjectCommandResult
                {
                    IsSuccess = false,
                    Errors = { ex.Message },
                    StatusCode = StatusCode.Error
                };
            }

        }
    }
}
