
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

            try
            {
                var result = await _completeArrival.SubmitAsync(model);

                if (result.ApplicationNo == Guid.Empty)
                {
                    return BadRequest(new { message = "Failed to Submit ArrivalApplication" });
                }

                return Ok(new
                {
                    message = "Application submitted successfully",
                    applicationNo = result.ApplicationNo,
                    referenceNo = result.ReferenceNo,
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An internal server error occurred." });
            }
        }


        [HttpGet("{AppNo}")]
        public async Task<IActionResult> GetDetails(Guid AppNo)
        {
            try
            {
                var applicationDetails = await _completeArrival.GetScanAsync(AppNo);

                if (applicationDetails == null)
                {
                    return NotFound(new { message = "Application not found or Invalid QR Code!" });
                }


                return Ok(applicationDetails);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new
                {
                    status = "Invalid",
                    message = ex.Message
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An internal error occurred." });
            }

        }
    }
}