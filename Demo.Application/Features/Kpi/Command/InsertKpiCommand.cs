using Demo.Application.Features.Kpi.Query;
using Demo.Domain.Common;
using Demo.Domain.Entities;
using Demo.Infrastructure.Contexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Features.Kpi.Command
{


    public record InsertKpiCommand(string Name) : IRequest<InsertKpiCommandResult>
    {
    }
    public record InsertKpiCommandResult : BaseCommonResult
    {
        public KpiVM Kpi { get; set; }
    }

    public class InsertKpiCommandHandler : IRequestHandler<InsertKpiCommand, InsertKpiCommandResult>
    {
        private readonly ApplicationDbContext _context;

        public InsertKpiCommandHandler(ApplicationDbContext context)
        {
            this._context=context;
        }
        public async Task<InsertKpiCommandResult> Handle(InsertKpiCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var kpi = new Domain.Entities.Kpi 
                {
                    CreatedAt = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    UpdatedAt = DateTime.UtcNow,
                };
                await _context.Kpis.AddAsync(kpi);
                await _context.SaveChangesAsync();
                return new InsertKpiCommandResult 
                {
                    IsSuccess = true,
                    Kpi =  new KpiVM
                    {
                        Id = kpi.Id,
                        Name = kpi.Name,
                    },
                };
            }
            catch (Exception ex)
            {
                return new InsertKpiCommandResult
                {
                    IsSuccess = false,
                    Errors = { ex.Message },
                    StatusCode = StatusCode.Error
                };
            }

        }
    }

}
