using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MMAC.Data;
using MMAC.DTOS;

namespace MMAC.Services.SearchService
{
    public class MyanmarSearchService : IMyanmarSearchService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MyanmarSearchService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseMyanmarArrivalDTO> SearchMyanmarDetailsAsync(MyanmarVerifyRequestDTO dto)
        {
            var app = await _context.ArrivalApplication
                .Include(a => a.Traveller)
                .Include(a => a.selectedModeOfTravel)
                .Include(a => a.selectedPortOfArrival)
                .Include(a => a.Township)
                .OrderByDescending(a => a.CreatedDate)
                .FirstOrDefaultAsync(a => a.ReferenceNo.ToLower() == dto.ReferenceNo.ToLower()
                                       && a.AppStatus == "Submitted");


            if (app == null || app.Traveller == null)
            {
                throw new KeyNotFoundException("The provided verification details do not match any registered Myanmar citizen records.");
            }


            bool isIdentityMatched = app.Traveller.NRC != null
                                  && app.Traveller.NRC.ToLower() == dto.NRC.ToLower()
                                  && app.ArrivalDate.Date == dto.ArrivalDate.Date;

            if (!isIdentityMatched)
            {
                throw new KeyNotFoundException("The provided verification details do not match any registered Myanmar citizen records.");
            }


            if (app.Township != null)
            {
                await _context.Entry(app.Township).Reference(t => t.District).LoadAsync();
                if (app.Township.District != null)
                {
                    await _context.Entry(app.Township.District).Reference(d => d.StateRegion).LoadAsync();
                }
            }


            var result = _mapper.Map<ResponseMyanmarArrivalDTO>(app);
            _mapper.Map(app.Traveller, result);


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