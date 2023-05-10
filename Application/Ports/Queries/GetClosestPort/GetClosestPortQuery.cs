using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Ports.Queries.GetClosestPort
{
    public record GetClosestPortQuery : IRequest<ClosestPortDto>
    {
        public Guid ShipId { get; set; }
    }

    internal sealed class GetClosestPortQueryHandler : IRequestHandler<GetClosestPortQuery, ClosestPortDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetClosestPortQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ClosestPortDto> Handle(GetClosestPortQuery request, CancellationToken cancellationToken)
        {
            var ship = await _context.Ships.FindAsync(new object[] { request.ShipId }, cancellationToken);

            if (ship == null)
            {
                throw new ApiApplicationException(HttpStatusCode.NotFound, Constants.ErrorMessages.SHIP_NOTFOUND);
            }

            if (ship.Location == null)
            {
                throw new ApiApplicationException(HttpStatusCode.NotFound, Constants.ErrorMessages.SHIP_LOCATION_NOTFOUND);
            }

            var closestPort = await _context.Ports
                .Where(p => p.Location != null)
                .OrderBy(p => p.Location.Distance(ship.Location))
                .FirstOrDefaultAsync();

            if (closestPort == null)
            {
                throw new ApiApplicationException(HttpStatusCode.NotFound, Constants.ErrorMessages.CLOSEST_PORT_NOTFOUND);
            }

            var result = _mapper.Map<ClosestPortDto>(closestPort);

            if (ship.Velocity != null)
            {
                var distance = ship.Location.Distance(closestPort.Location);
                result.EstimatedArrivalTime = DateTime.UtcNow.AddHours((double)(distance / ship.Velocity));
            }

            return result;
        }
    }
}
