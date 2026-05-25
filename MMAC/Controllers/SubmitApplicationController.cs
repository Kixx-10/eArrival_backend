
using Microsoft.AspNetCore.Mvc;
using MMAC.DTOS;
using MMAC.Services.ArrivalInterface;

namespace MMAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmitApplicationController : ControllerBase
    {
        private readonly ICompleteArrival _completeArrival;

        public SubmitApplicationController(ICompleteArrival completeArrival)
        {
            _completeArrival = completeArrival;
        }
        [HttpPost]
        public async Task<IActionResult> Submit([FromBody] CompleteArrivalDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var AppNo = await _completeArrival.SubmitAsync(model);

            if (AppNo == Guid.Empty)
            {
                return BadRequest(new { message = "Failed to Submit ArrivalApplication" });
            }

            return Ok(new
            {
                message = "Application submitted successfully",
                ApplicationNo = AppNo,
                qrCodeContent = AppNo.ToString()
            });
        }


        [HttpGet("{AppNo}")]
        public async Task<IActionResult> GetDetails(Guid AppNo)
        {
            var applicationDetails = await _completeArrival.GetScanAsync(AppNo);

            if (applicationDetails == null)
            {
                return NotFound(new { message = "Application not found or Invalid QR Code!" });
            }


            return Ok(applicationDetails);
        }
    }
}