using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MMAC.Interfaces;

namespace MMAC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;


        // 1. Dependency Injection pulls in your ICountryService effortlessly!
        public CountryController(ICountryService countryService)
        {
            _countryService = countryService;

        }


        [HttpGet("AllCountries")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPassportIssuedCountry()
        {
            var countries = await _countryService.GetAllCountresAsync();
            return Ok(countries);
        }

        [HttpGet("IcaoMemberCountries")]
        [AllowAnonymous]
        public async Task<IActionResult> GetNationalityCountry()
        {
            var countries = await _countryService.GetIcaoMemberCountriesAsync();
            return Ok(countries);
        }

        // 3. Endpoint for: GET api/country/{code}
        [HttpGet("{code}")]
        [AllowAnonymous]
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