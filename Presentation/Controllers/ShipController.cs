using Application.Ships.Commands.CreateShip;
using Application.Ships.Commands.UpdateShipVelocity;
using Application.Ships.Queries.GetAllShips;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/ships")]
    public class ShipController : BaseController
    {
        private readonly ILogger<ShipController> _logger;

        public ShipController(ILogger<ShipController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<ShipDto>>> GetAllShips()
        {
            var result = await Mediator.Send(new GetAllShipsQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateShip([FromBody] CreateShipCommand request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

        [HttpPut("{id:guid}/velocity")]
        public async Task<IActionResult> UpdateShip(Guid id, UpdateShipVelocityCommand request)
        {
            request.Id = id;
            await Mediator.Send(request);

            return NoContent();
        }
    }
}