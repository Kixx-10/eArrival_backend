using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MMAC.DTOS;
using MMAC.Repositories;

namespace MMAC.Services.ArrivalApplicationService
{
    //public class UpdateApplicationService : IUpdateAppliationService
    //{
    //    private readonly IUpdateAppliationService _repository;
    //    private readonly IMapper _mapper;

    //    // Dependency Injection အတွက် Constructor အပြည့်အစုံ
    //    public UpdateApplicationService(IUpdateAppliationService repository, IMapper mapper)
    //    {
    //        _repository = repository;
    //        _mapper = mapper;
    //    }

    //    public async Task<ServiceResponse<ResponseCompleteArrivalDTO>> GetCitizenApplication(string nric, DateTime arrivalDate)
    //    {
    //        var response = new ServiceResponse<ResponseCompleteArrivalDTO>();

    //        try
    //        {
    //            // ၁။ Input Validation ပြုလုပ်ခြင်း
    //            if (string.IsNullOrWhiteSpace(nric))
    //            {
    //                response.Success = false;
    //                response.Message = "NRIC cannot be empty.";
    //                return response;
    //            }

    //            if (arrivalDate == DateTime.MinValue)
    //            {
    //                response.Success = false;
    //                response.Message = "Invalid arrival date.";
    //                return response;
    //            }

    //            // ၂။ Repository မှတဆင့် Database ထဲတွင် Data ရှိမရှိ ရှာဖွေခြင်း
    //            var application = await _repository.GetByNricAndDateAsync(nric.Trim(), arrivalDate.Date);

    //            if (application == null)
    //            {
    //                response.Success = false;
    //                response.Message = "No arrival application found for the given NRIC and Arrival Date.";
    //                return response;
    //            }

    //            // ၃။ AutoMapper ကို အသုံးပြု၍ Entity မှ DTO သို့ အလိုအလျောက် ပြောင်းလဲခြင်း
    //            response.Data = _mapper.Map<ResponseCompleteArrivalDTO>(application);

    //            response.Success = true;
    //            response.Message = "Application retrieved successfully.";
    //        }
    //        catch (Exception ex)
    //        {
    //            // ၄။ Error တစ်စုံတစ်ရာ ရှိပါက ကာကွယ်ခြင်း
    //            response.Success = false;
    //            response.Message = $"An error occurred while fetching data: {ex.Message}";
    //        }

    //        return response;
    //    }
    //}
}