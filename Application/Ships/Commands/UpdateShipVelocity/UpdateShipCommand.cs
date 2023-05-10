using Application.Common;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models.Transaction;
using MediatR;
using System.Net;

namespace Application.Ships.Commands.UpdateShipVelocity
{
    public record UpdateShipVelocityCommand : IRequest, ITransactionalRequest
    {
        public Guid Id { get; set; }
        public double? Velocity { get; init; }
    }

    internal sealed class UpdateShipVelocityCommandHandler : IRequestHandler<UpdateShipVelocityCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateShipVelocityCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateShipVelocityCommand request, CancellationToken cancellationToken)
        {
            var ship = await _context.Ships.FindAsync(new object[] { request.Id }, cancellationToken);

            if (ship == null)
            {
                throw new ApiApplicationException(HttpStatusCode.NotFound, Constants.ErrorMessages.SHIP_NOTFOUND);
            }

            ship.Velocity = request.Velocity;
            _context.Ships.Update(ship);
        }
    }
}
