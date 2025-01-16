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

    public record GetAllProjectsQuery : IRequest<GetAllProjectsQueryResult>
    {
    }
    public record GetAllProjectsQueryResult : BaseCommonResult
    {
        public List<ProjectVM> Projects { get; set; }
    }
    public record ProjectVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
    }
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, GetAllProjectsQueryResult>
    {
        private readonly ApplicationDbContext _context;

        public GetAllProjectsQueryHandler(ApplicationDbContext context)
        {
            this._context=context;
        }
        public async Task<GetAllProjectsQueryResult> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var projects = await _context.Projects.Select(p => new ProjectVM
                {
                    Id = p.Id,
                    Name = p.Name,
                    StartDate = p.StartDate
                }).ToListAsync();

                return new GetAllProjectsQueryResult
                {
                    IsSuccess = true,
                    Projects = projects
                };
            }
            catch (Exception ex)
            {
                return new GetAllProjectsQueryResult
                {
                    IsSuccess = false,
                    Errors = { ex.Message },
                    StatusCode = StatusCode.Error
                };
            }

        }
    }
}
