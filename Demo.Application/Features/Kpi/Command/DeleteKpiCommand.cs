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

    public record DeleteKpiCommand(Guid Id) : IRequest<DeleteKpiCommandResult>
    {
    }
    public record DeleteKpiCommandResult : BaseCommonResult;

    public class DeleteKpiCommandHandler : IRequestHandler<DeleteKpiCommand, DeleteKpiCommandResult>
    {
        private readonly ApplicationDbContext _context;

        public DeleteKpiCommandHandler(ApplicationDbContext context)
        {
            this._context=context;
        }
        public async Task<DeleteKpiCommandResult> Handle(DeleteKpiCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var kpi = await _context.Kpis.Where(kpi => kpi.Id == request.Id).FirstOrDefaultAsync();

                if (kpi != null)
                {

                    _context.Kpis.Remove(kpi);
                    await _context.SaveChangesAsync();

                    return new DeleteKpiCommandResult()
                    {
                        IsSuccess = true,
                    };
                }

                return new DeleteKpiCommandResult()
                {
                    IsSuccess = false,
                    Errors = { $"This id {request.Id} Not Found" },
                    StatusCode = StatusCode.NotFound

                };
            }
            catch (Exception ex)
            {
                return new DeleteKpiCommandResult
                {
                    IsSuccess = false,
                    Errors = { ex.Message },
                    StatusCode = StatusCode.Error
                };
            }
        }
    }
}
