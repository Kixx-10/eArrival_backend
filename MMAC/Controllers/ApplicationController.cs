
using Microsoft.AspNetCore.Mvc;
using MMAC.DTOS;
using MMAC.Models.Cores;
using MMAC.Services.ArrivalInterface;
using MMAC.Services.PdfService;

namespace MMAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ICompleteArrivalService _completeArrival;
        private readonly IPdfService _pdfService;

        public ApplicationController(ICompleteArrivalService completeArrival, IPdfService pdfService)

        {
            _completeArrival = completeArrival;
            _pdfService = pdfService;
        }

        [HttpPost("Submit&UpdateApplication")]
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

                byte[] pdfBytes = await _pdfService.GenerateArrivalPdfAsync(model, result.ApplicationNo, result.ReferenceNo);

                if (!string.IsNullOrEmpty(model.Email))
                {
                    _pdfService.SendPdfEmailInBackground(model.Email, result.ApplicationNo.ToString(), pdfBytes,result.ReferenceNo,model.TravellerId);
                }

                string pdfBase64 = Convert.ToBase64String(pdfBytes);

                return Ok(new
                {
                    message = "Application submitted successfully",
                    applicationNo = result.ApplicationNo.ToString(),
                    referenceNo = result.ReferenceNo ?? "N/A",
                    pdfData = pdfBase64
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
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during submission or PDF processing.", error = ex.Message });
            }
        }


        [HttpGet("SearchApplicationByQRCode{AppNo}")]
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


        [HttpPost("ApproveApplication")]
        public async Task<IActionResult> ApproveApplication([FromBody] RequestUpdateStatusDTO request)
        {
            // Check if AppNo or AppStatus is missing
            if (request.AppNo == Guid.Empty || string.IsNullOrWhiteSpace(request.AppStatus))
            {
                return BadRequest(new { message = "AppNo and Status are required." });
            }

            try
            {
                bool isSuccess = await _completeArrival.ApproveApplication(request.AppNo, request.AppStatus, request.ApproveUser);

                // Check if the application exists
                if (!isSuccess)
                {
                    return NotFound(new { message = "Application not found." });
                }

                // Set the success message based on the status
                string responseMessage = request.AppStatus.Equals("Approved", StringComparison.OrdinalIgnoreCase)
                    ? "Application approved successfully."
                    : "Application rejected successfully.";

                return Ok(new
                {
                    message = responseMessage,
                    status = request.AppStatus
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the application status.", error = ex.Message });
            }
        }

    }
}