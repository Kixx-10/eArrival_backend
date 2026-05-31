using Microsoft.AspNetCore.Mvc;
using MMAC.Models.Master;
using MMAC.Repositories;

namespace MMAC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurposeOfVisitController : ControllerBase
    {
        private readonly IPurposeOfVisitRepository _repository;
        public PurposeOfVisitController(IPurposeOfVisitRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllPurposes()
        {
            var purposes = await _repository.GetAllPurposeOfVisitsAsync();

            if (purposes == null || !purposes.Any())
            {
                return NotFound(new { message = "No purposes of visit found." });
            }


            var result = purposes
                .OrderBy(p => p.PurposeOfVisitName)
                .Select(p => new
                {
                    PurposeOfVisitId = p.PurposeOfVisitId,
                    PurposeOfVisitName = p.PurposeOfVisitName
                })
                .ToList();

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreatePurpose([FromBody] PurposeOfVisit model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdEntity = await _repository.AddPurposeOfVisitAsync(model);

            if (createdEntity == null)
            {
                return StatusCode(500, "A error occurred while saving the purpose of visit.");
            }

            return Ok(createdEntity);
        }
    }
}