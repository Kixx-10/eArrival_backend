using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MMAC.Data;
using MMAC.DTOS;
using MMAC.Models.Cores;

namespace MMAC.Services.ArrivalInterface
{
    public class CompleteArrivalService : ICompleteArrival
    {
        private readonly AppDbContext _context;
        private IMapper _mapper;

        public CompleteArrivalService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Guid> SubmitAsync(CompleteArrivalDTO dto)
        {
            if (dto.CountryOfBirthCode == "MMR")
            {
                if (string.IsNullOrWhiteSpace(dto.NRC) || string.IsNullOrWhiteSpace(dto.FatherName))
                {
                    throw new ArgumentException("Need to fill NRC and FatherName for Myanmr Citizens");
                }
            }
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var traveller = _mapper.Map<Traveller>(dto);
                traveller.TravellerId = Guid.NewGuid();
                await _context.Traveller.AddAsync(traveller);

                //to store arrivalApplication or tips & accommondation
                var arrivalApplication = _mapper.Map<ArrivalApplication>(dto);
                arrivalApplication.AppNo = Guid.NewGuid();
                arrivalApplication.TravellerId = traveller.TravellerId;
                await _context.ArrivalApplication.AddAsync(arrivalApplication);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return arrivalApplication.AppNo;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                Console.WriteLine(ex.ToString());
                return Guid.Empty;
            }
        }
        public async Task<ResponseCompleteArrivalDTO?> GetScanAsync(Guid AppNo)
        {
            var app = await _context.ArrivalApplication
                .Include(x => x.Traveller)
                .Include(x => x.PurposeOfVisit)
                .Include(x => x.selectedModeOfTravel)
                .Include(x => x.selectedPortOfArrival)
                .Include(x => x.Township)
                    .ThenInclude(t => t!.District)
                        .ThenInclude(d => d!.StateRegion)
                .FirstOrDefaultAsync(a => a.AppNo == AppNo);

            if (app == null) return null;

            var result = _mapper.Map<ResponseCompleteArrivalDTO>(app);

            if (app.Traveller != null)
            {
                _mapper.Map(app.Traveller, result);
            }

            if (app.selectedModeOfTravel != null)
            {
                result.ModeOfTravelName = app.selectedModeOfTravel.ModeOfTravelName;
            }

            if (app.selectedPortOfArrival != null)
            {
                result.PortOfArrivalName = app.selectedPortOfArrival.PortOfArrivalName;
            }

            if (app.PurposeOfVisit != null)
            {
                result.PurposeOfVisitName = app.PurposeOfVisit.PurposeOfVisitName;
            }
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
