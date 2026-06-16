using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using MMAC.DTOS;
using MMAC.Services.ArrivalApplicationService;

namespace MMAC.Controllers
{

    //[Route("api/[controller]")]
    //[ApiController]

    //public class UpdateApplicationController : ControllerBase
    //{
    //    private readonly IUpdateAppliationService _updateAppliationService;

    //    public UpdateApplicationController(IUpdateAppliationService updateAppliationService)
    //    {
    //        _updateAppliationService = updateAppliationService;
    //    }
    //    [HttpGet("GetCitizenForm/{nric}/{arrivalDate}")]
    //    public async Task<ActionResult<ServiceResponse<ResponseCompleteArrivalDTO>>> GetCitizenApplication(string nric, DateTime arrivalDate)
    //    {
    //        try
    //        {
    //            var response = await _updateAppliationService.GetCitizenApplication(nric, arrivalDate);
    //            if (response.Success)
    //            {
    //                return Ok(response);
    //            }
    //            return BadRequest(response);
    //        }
    //        catch (Exception ex)
    //        {
    //            return StatusCode(500, "Internal server error");
    //        }
    //    }
    //}
}
