using Microsoft.AspNetCore.Mvc;
using MMAC.Interfaces;
using MMAC.Models.Master;

namespace MMAC.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // This turns your URL route into: /api/country
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;

        // 1. Dependency Injection pulls in your ICountryService effortlessly!
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        // 2. Endpoint for: GET api/country
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var countries = await _countryService.GetAllCountriesAsync();
            return Ok(countries);
        }

        // 3. Endpoint for: GET api/country/{code}
        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            var country = await _countryService.GetCountryByCodeAsync(code);

            if (country == null)
            {
                // Returns a clean 404 response with a clear message if code doesn't exist
                return NotFound(new { Message = $"Country with code '{code}' not found." });
            }

            return Ok(country);
        }
    }
}