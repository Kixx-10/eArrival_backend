using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MMAC.Data;
using MMAC.DTOS;
using MMAC.Repositories;

namespace MMAC.Services.UpdateService
{
    public class ForeignerSearchService : IForeignerSearchService
    {
        private readonly ICompleteArrivalRepository _repository;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ForeignerSearchService(ICompleteArrivalRepository repository, AppDbContext context, IMapper mapper)
        {
            _repository = repository;
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseForeignerArrivalDTO> SearchForeignerDetailsAsync(ForeignerVerifyRequestDTO dto)
        {
            // Identity ၅ ချက်လုံး ကွက်တိတိုက်စစ်ပြီး Master Tables အားလုံးကို Include ဆွဲခြင်း
            var app = await _context.ArrivalApplication
                .Include(a => a.Traveller)
                .Include(a => a.selectedModeOfTravel)
                .Include(a => a.selectedPortOfArrival)
                .Include(a => a.Township)
                    .ThenInclude(t => t!.District)
                        .ThenInclude(d => d!.StateRegion)
                .FirstOrDefaultAsync(a => a.ReferenceNo.ToLower() == dto.ReferenceNo.ToLower()
                                       && a.Traveller!.PassportNo.ToLower() == dto.PassportNo.ToLower()
                                       && a.Traveller.CountryOfBirthCode.ToLower() == dto.CountryOfBirthCode.ToLower()
                                       && a.Traveller.ExpiryDate.Date == dto.ExpiryDate.Date
                                       && a.Traveller.DOB.Date == dto.DOB.Date);

            if (app == null)
            {
                throw new KeyNotFoundException("The provided verification details do not match any registered foreign national records.");
            }

            if (app.Traveller != null && app.Traveller.CountryOfBirthCode == "MMR")
            {
                throw new InvalidOperationException("Access Denied. This endpoint is restricted for foreign nationals only.");
            }

            var result = _mapper.Map<ResponseForeignerArrivalDTO>(app);
            if (app.Traveller != null) _mapper.Map(app.Traveller, result);

            // Master Names ဖြည့်သွင်းခြင်း
            if (app.selectedModeOfTravel != null) result.ModeOfTravelName = app.selectedModeOfTravel.ModeOfTravelName;
            if (app.selectedPortOfArrival != null) result.PortOfArrivalName = app.selectedPortOfArrival.PortOfArrivalName;
            if (app.Township != null)
            {
                result.TownshipName = app.Township.Name;
                if (app.Township.District != null)
                {
                    result.DistrictName = app.Township.District.Name;
                    if (app.Township.District.StateRegion != null)
                    {
                        result.StateRegionName = app.Township.District.StateRegion.Name;
                    }
                }
            }

            return result;
        }

    }
}