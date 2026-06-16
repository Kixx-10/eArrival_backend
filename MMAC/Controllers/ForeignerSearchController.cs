using Microsoft.AspNetCore.Mvc;
using MMAC.DTOS;
using MMAC.Services.UpdateService;

namespace MMAC.Controllers
{
    [ApiController]
    [Route("api/foreigner")]
    public class ForeignerSearchController : ControllerBase
    {
        private readonly IForeignerSearchService _foreignerSearchService;

        public ForeignerSearchController(IForeignerSearchService foreignerSearchService)
        {
            _foreignerSearchService = foreignerSearchService;
        }

        [HttpPost("verify-and-retrieve")]
        public async Task<IActionResult> VerifyAndRetrieve([FromBody] ForeignerVerifyRequestDTO requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest(new { success = false, error = "Request body cannot be empty." });
            }

            try
            {
                // Service Layer ထံမှတစ်ဆင့် ၅ ချက်လုံးကို တိုက်စစ်ပြီး ဒေတာဆွဲထုတ်ခြင်း
                ResponseForeignerArrivalDTO foreignerData = await _foreignerSearchService.SearchForeignerDetailsAsync(requestDto);
                return Ok(new
                {
                    success = true,
                    message = "Verification successful. Existing data retrieved.",
                    data = foreignerData
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    success = false,
                    error = ex.Message
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CONTROLLER ERROR]: {ex.Message}");
                return StatusCode(500, new
                {
                    success = false,
                    error = "An internal server error occurred while processing your request."
                });
            }
        }
    }
}