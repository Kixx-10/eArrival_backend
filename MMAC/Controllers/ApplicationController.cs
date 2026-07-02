
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using MMAC.DTOS;
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
                BackgroundJob.Schedule<ICompleteArrivalService>(
                    service => service.AutoExpireApplicationAsync(result.ApplicationNo),
                    TimeSpan.FromMinutes(3)
                );

                if (result.ApplicationNo == Guid.Empty)
                {
                    return BadRequest(new { message = "Failed to Submit ArrivalApplication" });
                }

                byte[] pdfBytes = await _pdfService.GenerateArrivalPdfAsync(model, result.ApplicationNo, result.ReferenceNo);
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
        
        [HttpPost("SendApplicationEmail")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailRequestDTO request)
        {
            if (request == null || request.Model == null)
            {
                return BadRequest(new { message = "Invalid request data." });
            }

            string emailToSend = !string.IsNullOrEmpty(request.TargetEmail) ? request.TargetEmail : request.Model.Email;

            if (string.IsNullOrEmpty(emailToSend))
            {
                return BadRequest(new { message = "Recipient email address is required." });
            }

            if (request.ApplicationNo == Guid.Empty || string.IsNullOrEmpty(request.ReferenceNo))
            {
                return BadRequest(new { message = "ApplicationNo and ReferenceNo are required." });
            }

            try
            {
                byte[] pdfBytes = await _pdfService.GenerateArrivalPdfAsync(request.Model, request.ApplicationNo, request.ReferenceNo);

                _pdfService.SendPdfEmailInBackground(emailToSend, request.ApplicationNo.ToString(), pdfBytes, request.ReferenceNo, request.Model.TravellerId);

                return Ok(new
                {
                    message = $"PDF email submission initiated successfully for {emailToSend}",
                    status = "Success"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while sending the email.", error = ex.Message });
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