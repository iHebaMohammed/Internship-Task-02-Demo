using Demo.Application.Features.Location.Query;
using Demo.Domain.Common;
using Demo.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Features.Project.Ouery
{

    public record GetProjectByIdQuery(Guid Id) : IRequest<GetProjectByIdQueryResult>
    {
    }
    public record GetProjectByIdQueryResult : BaseCommonResult
    {
        public ProjectVM Project { get; set; }
    }

    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, GetProjectByIdQueryResult>
    {
        private readonly ApplicationDbContext _context;

        public GetProjectByIdQueryHandler(ApplicationDbContext context)
        {
            this._context=context;
        }
        public async Task<GetProjectByIdQueryResult> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var project = await _context.Projects
                    .Where(p => p.Id == request.Id)
                    .Select(p => new ProjectVM
                    {
                        Id = p.Id,
                        Name = p.Name,
                        StartDate = p.StartDate
                    })
                    .FirstOrDefaultAsync();

                if (project == null)
                    return new GetProjectByIdQueryResult
                    {
                        IsSuccess = true,
                        Errors = { $"This Id {request.Id} Not Found" },
                        StatusCode = StatusCode.NotFound
                    };

                return new GetProjectByIdQueryResult
                {
                    IsSuccess = true,
                    Project = project,
                };
            }
            catch (Exception ex)
            {
                return new GetProjectByIdQueryResult
                {
                    IsSuccess = false,
                    Errors = { ex.Message },
                    StatusCode = StatusCode.Error
                };
            }

        }
    }
}
