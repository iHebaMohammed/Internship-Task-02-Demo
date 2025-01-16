using Demo.Application.Features.Kpi.Query;
using Demo.Domain.Common;
using Demo.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Features.Kpi.Command
{
    public record UpdateKpiCommand(Guid Id, string Name) : IRequest<UpdateKpiCommandResult>
    {
    }
    public record UpdateKpiCommandResult : BaseCommonResult
    {
        public KpiVM Kpi { get; set; }
    }

    public class UpdateKpiCommandHandler : IRequestHandler<UpdateKpiCommand, UpdateKpiCommandResult>
    {
        private readonly ApplicationDbContext _context;

        public UpdateKpiCommandHandler(ApplicationDbContext context)
        {
            this._context=context;
        }
        public async Task<UpdateKpiCommandResult> Handle(UpdateKpiCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var kpi = await _context.Kpis.Where(kpi => kpi.Id == request.Id).FirstOrDefaultAsync();
                
                if (kpi != null)
                {
                    kpi.Name = request.Name;
                    kpi.UpdatedAt = DateTime.Now;
                    
                    _context.Kpis.Update(kpi);
                    await _context.SaveChangesAsync();

                    return new UpdateKpiCommandResult()
                    {
                        IsSuccess = true,
                        Kpi = new KpiVM 
                        {
                            Id = request.Id,
                            Name = request.Name,
                        }
                    };
                }

                return new UpdateKpiCommandResult()
                {
                    IsSuccess = false,
                    Errors = { $"This id {request.Id} Not Found" },
                    StatusCode = StatusCode.NotFound

                };
            }
            catch (Exception ex)
            {
                return new UpdateKpiCommandResult
                {
                    IsSuccess = false,
                    Errors = { ex.Message },
                    StatusCode = StatusCode.Error
                };
            }
        }
    }
}
