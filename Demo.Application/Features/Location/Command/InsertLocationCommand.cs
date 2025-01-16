using Demo.Application.Features.Kpi.Query;
using Demo.Application.Features.Location.Query;
using Demo.Domain.Common;
using Demo.Infrastructure.Contexts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Features.Location.Command
{
    public record InsertLocationCommand(string Name) : IRequest<IInsertLocationCommandResult>
    {
    }
    public record IInsertLocationCommandResult : BaseCommonResult
    {
        public LocationVM Location { get; set; }
    }

    public class InsertLocationCommandHandler : IRequestHandler<InsertLocationCommand, IInsertLocationCommandResult>
    {
        private readonly ApplicationDbContext _context;

        public InsertLocationCommandHandler(ApplicationDbContext context)
        {
            this._context=context;
        }
        public async Task<IInsertLocationCommandResult> Handle(InsertLocationCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var location = new Domain.Entities.Location
                {
                    CreatedAt = DateTime.UtcNow,
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    UpdatedAt = DateTime.UtcNow,
                };
                await _context.Locations.AddAsync(location);
                await _context.SaveChangesAsync();
                return new IInsertLocationCommandResult
                {
                    IsSuccess = true,
                    Location =  new LocationVM
                    {
                        Id = location.Id,
                        Name = location.Name,
                    },
                };
            }
            catch (Exception ex)
            {
                return new IInsertLocationCommandResult
                {
                    IsSuccess = false,
                    Errors = { ex.Message },
                    StatusCode = StatusCode.Error
                };
            }

        }
    }
}
