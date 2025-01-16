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

namespace Demo.Application.Features.Location.Query
{


    public record GetLocationByIdQuery(Guid Id) : IRequest<GetLocationByIdQueryResult>
    {
    }
    public record GetLocationByIdQueryResult : BaseCommonResult
    {
        public LocationVM Location { get; set; }
    }

    public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, GetLocationByIdQueryResult>
    {
        private readonly ApplicationDbContext _context;

        public GetLocationByIdQueryHandler(ApplicationDbContext context)
        {
            this._context=context;
        }
        public async Task<GetLocationByIdQueryResult> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var location = await _context.Locations
                    .Where(location => location.Id == request.Id)
                    .Select(location => new LocationVM
                    {
                        Id = location.Id,
                        Name = location.Name
                    })
                    .FirstOrDefaultAsync();

                if (location == null)
                    return new GetLocationByIdQueryResult
                    {
                        IsSuccess = true,
                        Errors = { $"This Id {request.Id} Not Found" },
                        StatusCode = StatusCode.NotFound
                    };

                return new GetLocationByIdQueryResult
                {
                    IsSuccess = true,
                    Location = location,
                };
            }
            catch (Exception ex)
            {
                return new GetLocationByIdQueryResult
                {
                    IsSuccess = false,
                    Errors = { ex.Message },
                    StatusCode = StatusCode.Error
                };
            }

        }
    }
}