using Application.Common.Interfaces;
using Application.Common.Models.Transaction;
using Domain.Entities;
using MediatR;
using NetTopologySuite.Geometries;

namespace Application.Ships.Commands.CreateShip
{
    public record CreateShipCommand : IRequest<Guid>, ITransactionalRequest
    {
        public string Name { get; init; }
        public double? Velocity { get; init; }
        public LatLongDto? Location { get; init; }
    }

    public record LatLongDto
    {
        public double? Latitude { get; init; }
        public double? Longitude { get; init; }
    }

    public sealed class CreateShipCommandHandler : IRequestHandler<CreateShipCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateShipCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateShipCommand request, CancellationToken cancellationToken)
        {
            var ship = new Ship(request.Name)
            {
                Location = request.Location != null
                    ? new Point((double)request.Location.Latitude, (double)request.Location.Longitude)
                    : null,
                Velocity = request.Velocity
            };

            await _context.Ships.AddAsync(ship);

            return ship.Id;
        }
    }
}
