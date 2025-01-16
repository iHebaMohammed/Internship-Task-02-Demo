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
    public record GetKpiByIdQuery(Guid Id) : IRequest<GetKpiByIdQueryResult>
    {
    }
    public record GetKpiByIdQueryResult : BaseCommonResult
    {
        public KpiVM Kpi { get; set; }
    }

    public class GetKpiByIdQueryHandler : IRequestHandler<GetKpiByIdQuery, GetKpiByIdQueryResult>
    {
        private readonly ApplicationDbContext _context;

        public GetKpiByIdQueryHandler(ApplicationDbContext context)
        {
            this._context=context;
        }
        public async Task<GetKpiByIdQueryResult> Handle(GetKpiByIdQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var kpi = await _context.Kpis
                    .Where(kpi => kpi.Id == request.Id)
                    .Select(kpi => new KpiVM {
                        Id = kpi.Id ,
                        Name = kpi.Name
                    })
                    .FirstOrDefaultAsync();

                if (kpi == null)
                    return new GetKpiByIdQueryResult 
                    {
                        IsSuccess = true,
                        Errors = { $"This Id {request.Id} Not Found" },
                        StatusCode = StatusCode.NotFound
                    };

                return new GetKpiByIdQueryResult
                {
                    IsSuccess = true,
                    Kpi = kpi,
                };
            }
            catch (Exception ex)
            {
                return new GetKpiByIdQueryResult
                {
                    IsSuccess = false,
                    Errors = { ex.Message },
                    StatusCode = StatusCode.Error
                };
            }

        }
    }
}
