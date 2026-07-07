using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMAC.Services.PortOfArrivalService;

namespace MMAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortOfArrivalController : ControllerBase
    {
        private readonly IPortOfArrivalService _portOfArrivalService;

        public PortOfArrivalController(IPortOfArrivalService portOfArrivalService)
        {
            _portOfArrivalService = portOfArrivalService;
        }

        [HttpGet("{modeOfTravelId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPortsByMode(int modeOfTravelId)
        {
            var ports = await _portOfArrivalService.GetPortsByModeOfTravelAsync(modeOfTravelId);

            if (ports == null || !ports.Any())
            {
                return NotFound(new { message = "No ports found for this mode of travel." });
            }

            var result = ports
                .OrderBy(p => p.PortOfArrivalName)
                .Select(p => new
                {
                    PortOfArrivalId = p.PortOfArrivalId,
                    PortOfArrivalName = p.PortOfArrivalName,
                    ModeOfTravelId = p.ModeOfTravelId,
                    ModeOfTravelName = p.ModeOfTravel?.ModeOfTravelName
                })
                .ToList();

            return Ok(result);
        }
    }
}