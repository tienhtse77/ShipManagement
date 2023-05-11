using Application.Ports.Queries.GetClosestPort;
using Application.Ships.Queries.GetAllShips;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/ports")]
    public class PortController : BaseController
    {
        private readonly ILogger<PortController> _logger;

        public PortController(ILogger<PortController> logger)
        {
            _logger = logger;
        }

        [HttpGet("closest")]
        public async Task<ActionResult<List<ShipDto>>> GetAllShips([FromQuery] GetClosestPortQuery request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }
    }
}