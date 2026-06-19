using Microsoft.AspNetCore.Mvc;
using MMAC.Services.DashboardService;

namespace MMAC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(IDashboardService service) => _service = service;

        [HttpGet("GetDashboardData")]
            public async Task<IActionResult> GetDashboardData([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
            {
                var data = await _service.GetDashboardDataAsync(fromDate, toDate);
                return Ok(data);
            }
    }
}