using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MMAC.DTOS;
using MMAC.Services.UtilityService;

namespace MMAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UtilityController : Controller

    {
        private readonly IUtilityService _utilityService;

        public UtilityController(IUtilityService utilityService)
        {
            _utilityService = utilityService;
        }

        [HttpGet("Getlocations")]
        public async Task<ActionResult<IEnumerable<LocationDTO>>> GetLocations()
        {
            try
            {
                var locations = await _utilityService.GetLocations();

                return Ok(locations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the locations.");
            }
        }

        [HttpGet("GetNRCFormat")]
        public async Task<ActionResult<IEnumerable<NrcDTO>>> GetNrcFormat()
        {
            try
            {
                var nrcs = await _utilityService.GetNrcFormat();
                return Ok(nrcs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the NRC formats.");
            }
        }
    }
}       

         
          
   