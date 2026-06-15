
using Microsoft.AspNetCore.Mvc;
using MMAC.DTOS;
using MMAC.Services.ArrivalInterface;
using MMAC.Services.PdfService;

namespace MMAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmitApplicationController : ControllerBase
    {
        private readonly ICompleteArrival _completeArrival;
        private readonly IPdfService _pdfService;

        public SubmitApplicationController(ICompleteArrival completeArrival, IPdfService pdfService)
        {
            _completeArrival = completeArrival;
            _pdfService = pdfService;
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
                // ၁။ Database ထဲသို့ လျှောက်လွှာ အရင်သိမ်းမည်
                var result = await _completeArrival.SubmitAsync(model);

                if (result.ApplicationNo == Guid.Empty)
                {
                    return BadRequest(new { message = "Failed to Submit ArrivalApplication" });
                }

                // ၂။ FreeSpire.PDF သုံးထားသော Service မှ PDF အလိုအလျောက် ထုတ်ယူခြင်း
                byte[] pdfBytes = await _pdfService.GenerateArrivalPdfAsync(model, result.ApplicationNo, result.ReferenceNo);

                // ၃။ Email ကို Default အနေဖြင့် နောက်ကွယ်မှ တစ်ပါတည်း လှမ်းပို့ခြင်း
                if (!string.IsNullOrEmpty(model.Email))
                {
                    _pdfService.SendPdfEmailInBackground(model.Email, result.ApplicationNo.ToString(), pdfBytes);
                }

                // ၄။ [Business Logic သစ်] PDF Bytes ကို Base64 စာသားအဖြစ် ပြောင်းလဲခြင်း
                string pdfBase64 = Convert.ToBase64String(pdfBytes);

                // ၅။ JSON Response ထဲတွင် အားလုံးကို ပေါင်းစပ်၍ တစ်ခါတည်း ပြန်ပေးခြင်း
                return Ok(new
                {
                    message = "Application submitted successfully",
                    applicationNo = result.ApplicationNo.ToString(),
                    referenceNo = result.ReferenceNo ?? "N/A",
                    pdfData = pdfBase64 // Base64 String အဖြစ် ပါဝင်သွားမည်
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
                // PDF processing သို့မဟုတ် အခြားနေရာက အမှားများကို ဖမ်းရန်
                return StatusCode(500, new { message = "An error occurred during submission or PDF processing.", error = ex.Message });
            }
        }



        //[HttpPost]
        //public async Task<IActionResult> Submit([FromBody] CompleteArrivalDTO model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //        //}
        //        //if (!ModelState.IsValid)
        //        //{
        //        //    Console.WriteLine("======= [API MODEL STATE ERROR] =======");
        //        //    foreach (var modelState in ModelState)
        //        //    {
        //        //        var propertyName = modelState.Key;
        //        //        var errors = modelState.Value.Errors;

        //        //        foreach (var error in errors)
        //        //        {
        //        //            Console.WriteLine($"Field: {propertyName} -> Error: {error.ErrorMessage}");
        //        //            if (error.Exception != null)
        //        //            {
        //        //                Console.WriteLine($"Exception: {error.Exception.Message}");
        //        //            }
        //        //        }
        //        //    }
        //        Console.WriteLine("=======================================");

        //        return BadRequest(ModelState);
        //    }

        //    var result = await _completeArrival.SubmitAsync(model);

        //    if (result.ApplicationNo == Guid.Empty)
        //    {
        //        return BadRequest(new { message = "Failed to Submit ArrivalApplication" });
        //    }

        //    return Ok(new
        //    {
        //        message = "Application submitted successfully",
        //        applicationNo = result.ApplicationNo,
        //        referenceNo = result.ReferenceNo,
        //    });
        //}


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