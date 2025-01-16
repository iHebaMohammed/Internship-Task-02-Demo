using Demo.Domain.Common;
using Demo.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Features.Kpi.Query
{
    public record GetAllKpisQuery : IRequest<GetAllKpisQueryResult>
    {
    }
    public record GetAllKpisQueryResult : BaseCommonResult
    {
        public List<KpiVM> Kpis { get; set; }
    }
    public record KpiVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class GetAllKpisQueryHandler : IRequestHandler<GetAllKpisQuery, GetAllKpisQueryResult>
    {
        private readonly ApplicationDbContext _context;

        public GetAllKpisQueryHandler(ApplicationDbContext context)
        {
            this._context=context;
        }
        public async Task<GetAllKpisQueryResult> Handle(GetAllKpisQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var kpis = await _context.Kpis.Select(kpi => new KpiVM
                {
                    Id = kpi.Id,
                    Name = kpi.Name
                }).ToListAsync();

                return new GetAllKpisQueryResult 
                {
                    IsSuccess = true,
                    Kpis = kpis 
                };
            }
            catch (Exception ex) 
            {
                return new GetAllKpisQueryResult 
                {
                    IsSuccess = false,
                    Errors = { ex.Message },
                    StatusCode = StatusCode.Error
                };
            }

        }
    }
}
