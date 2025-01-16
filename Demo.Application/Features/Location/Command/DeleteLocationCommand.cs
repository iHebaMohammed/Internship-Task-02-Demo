using Demo.Domain.Common;
using Demo.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Features.Location.Command
{
    public record DeleteLocationCommand(Guid Id) : IRequest<DeleteLocationCommandResult>
    {
    }
    public record DeleteLocationCommandResult : BaseCommonResult;

    public class DeleteLocationCommandHandler : IRequestHandler<DeleteLocationCommand, DeleteLocationCommandResult>
    {
        private readonly ApplicationDbContext _context;

        public DeleteLocationCommandHandler(ApplicationDbContext context)
        {
            this._context=context;
        }
        public async Task<DeleteLocationCommandResult> Handle(DeleteLocationCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var location = await _context.Locations.Where(Location => Location.Id == request.Id).FirstOrDefaultAsync();

                if (location != null)
                {

                    _context.Locations.Remove(location);
                    await _context.SaveChangesAsync();

                    return new DeleteLocationCommandResult()
                    {
                        IsSuccess = true,
                    };
                }

                return new DeleteLocationCommandResult()
                {
                    IsSuccess = false,
                    Errors = { $"This id {request.Id} Not Found" },
                    StatusCode = StatusCode.NotFound

                };
            }
            catch (Exception ex)
            {
                return new DeleteLocationCommandResult
                {
                    IsSuccess = false,
                    Errors = { ex.Message },
                    StatusCode = StatusCode.Error
                };
            }
        }
    }
}
