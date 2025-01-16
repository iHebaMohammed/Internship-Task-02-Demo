using Demo.Application.Features.Kpi.Query;
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

namespace Demo.Application.Features.Location.Command
{
    public record UpdateLocationCommand(Guid Id, string Name) : IRequest<UpdateLocationCommandResult>
    {
    }
    public record UpdateLocationCommandResult : BaseCommonResult
    {
        public LocationVM Location { get; set; }
    }

    public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, UpdateLocationCommandResult>
    {
        private readonly ApplicationDbContext _context;

        public UpdateLocationCommandHandler(ApplicationDbContext context)
        {
            this._context=context;
        }
        public async Task<UpdateLocationCommandResult> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var location = await _context.Locations.Where(location => location.Id == request.Id).FirstOrDefaultAsync();

                if (location != null)
                {
                    location.Name = request.Name;
                    location.UpdatedAt = DateTime.Now;

                    _context.Locations.Update(location);
                    await _context.SaveChangesAsync();

                    return new UpdateLocationCommandResult()
                    {
                        IsSuccess = true,
                        Location = new LocationVM
                        {
                            Id = request.Id,
                            Name = request.Name,
                        }
                    };
                }

                return new UpdateLocationCommandResult()
                {
                    IsSuccess = false,
                    Errors = { $"This id {request.Id} Not Found" },
                    StatusCode = StatusCode.NotFound

                };
            }
            catch (Exception ex)
            {
                return new UpdateLocationCommandResult
                {
                    IsSuccess = false,
                    Errors = { ex.Message },
                    StatusCode = StatusCode.Error
                };
            }
        }
    }
}
