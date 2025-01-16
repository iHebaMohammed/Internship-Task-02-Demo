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

    public record DeleteProjectCommand(Guid Id) : IRequest<DeleteProjectCommandResult>
    {
    }
    public record DeleteProjectCommandResult : BaseCommonResult;

    public class DeleteLocationCommandHandler : IRequestHandler<DeleteProjectCommand, DeleteProjectCommandResult>
    {
        private readonly ApplicationDbContext _context;

        public DeleteLocationCommandHandler(ApplicationDbContext context)
        {
            this._context=context;
        }
        public async Task<DeleteProjectCommandResult> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var project = await _context.Projects.Where(p => p.Id == request.Id).FirstOrDefaultAsync();

                if (project != null)
                {

                    _context.Projects.Remove(project);
                    await _context.SaveChangesAsync();

                    return new DeleteProjectCommandResult()
                    {
                        IsSuccess = true,
                    };
                }

                return new DeleteProjectCommandResult()
                {
                    IsSuccess = false,
                    Errors = { $"This id {request.Id} Not Found" },
                    StatusCode = StatusCode.NotFound

                };
            }
            catch (Exception ex)
            {
                return new DeleteProjectCommandResult
                {
                    IsSuccess = false,
                    Errors = { ex.Message },
                    StatusCode = StatusCode.Error
                };
            }
        }
    }
}
