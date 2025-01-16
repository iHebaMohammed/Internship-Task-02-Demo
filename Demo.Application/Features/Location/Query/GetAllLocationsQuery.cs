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
    public record GetAllLocationsQuery : IRequest<GetAllLocationsQueryResult>
    {
    }
    public record GetAllLocationsQueryResult : BaseCommonResult
    {
        public List<LocationVM> Locations { get; set; }
    }
    public record LocationVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class GetAllLocationsQueryHandler : IRequestHandler<GetAllLocationsQuery, GetAllLocationsQueryResult>
    {
        private readonly ApplicationDbContext _context;

        public GetAllLocationsQueryHandler(ApplicationDbContext context)
        {
            this._context=context;
        }
        public async Task<GetAllLocationsQueryResult> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var locations = await _context.Locations.Select(location => new LocationVM
                {
                    Id = location.Id,
                    Name = location.Name
                }).ToListAsync();

                return new GetAllLocationsQueryResult
                {
                    IsSuccess = true,
                    Locations = locations
                };
            }
            catch (Exception ex)
            {
                return new GetAllLocationsQueryResult
                {
                    IsSuccess = false,
                    Errors = { ex.Message },
                    StatusCode = StatusCode.Error
                };
            }

        }
    }
}
