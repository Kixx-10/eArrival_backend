using Microsoft.AspNetCore.Mvc;
using MMAC.DTOS;
using MMAC.Services.SearchService;
using MMAC.Services.UpdateService;

namespace MMAC.Controllers
{
    [ApiController]
    [Route("api/searchDetails")]
    public class SearchController : ControllerBase
    {
        private readonly IForeignerSearchService _foreignerSearchService;
        private readonly IMyanmarSearchService _myanmarSearchService;

        public SearchController(
            IForeignerSearchService foreignerSearchService,
            IMyanmarSearchService myanmarSearchService)
        {
            _foreignerSearchService = foreignerSearchService;
            _myanmarSearchService = myanmarSearchService;
        }
        //For Foreigner
        [HttpPost("foreignerDetails")]
        public async Task<IActionResult> VerifyAndRetrieveForeigner([FromBody] ForeignerVerifyRequestDTO requestDto)
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
        //For Myanmar

        [HttpPost("myanmarDetails")]
        public async Task<IActionResult> VerifyAndRetrieveMyanmar([FromBody] MyanmarVerifyRequestDTO requestDto)
        {
            if (requestDto == null)
            {
                return BadRequest(new { success = false, error = "Request body cannot be empty." });
            }

            try
            {
                // Service Layer ထံမှတစ်ဆင့် 2 ချက်လုံးကို တိုက်စစ်ပြီး ဒေတာဆွဲထုတ်ခြင်း
                ResponseMyanmarArrivalDTO myanmarData = await _myanmarSearchService.SearchMyanmarDetailsAsync(requestDto);
                return Ok(new
                {
                    success = true,
                    message = "Verification successful. Existing data retrieved.",
                    data = myanmarData
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