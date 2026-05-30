using Microsoft.AspNetCore.Mvc;
using MMAC.Models.Master;
using MMAC.Services.PortOfArrivalService;

namespace MMAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurposeOfVisitController : ControllerBase
    {
        private readonly IPurposeOfVisitService _purposeOfVisitService;
        public PurposeOfVisitController(IPurposeOfVisitService purposeOfVisitService)
        {
            _purposeOfVisitService = purposeOfVisitService;
        }
        //createPurpose
        [HttpPost]
        public async Task<ActionResult> createPurposeOfVisit([FromBody] PurposeOfVisit model)
        {
            var reponse = await _purposeOfVisitService.CreatePurposeOfVisitAsync(model);
            if (reponse == null)
            {
                return BadRequest(new { message = "Failed to create purpose" });
            }
            return Ok(reponse);
        }
        [HttpGet]
        public async Task<ActionResult> getPurposeOfVisit()
        {
            var records = await _purposeOfVisitService.GetPurposeOfVisitAsync();
            var names = records.Select(x => x.PurposeOfVisitName).ToList();
            return Ok(names);
        }
    }
}
