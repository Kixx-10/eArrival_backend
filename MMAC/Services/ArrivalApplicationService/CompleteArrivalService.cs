using AutoMapper;
using MMAC.DTOS;
using MMAC.Models.Cores;
using MMAC.Repositories;

namespace MMAC.Services.ArrivalInterface
{
    public class CompleteArrivalService : ICompleteArrival
    {
        private readonly ICompleteArrivalRepository _repository;
        private readonly IMapper _mapper;

        public CompleteArrivalService(ICompleteArrivalRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> SubmitAsync(CompleteArrivalDTO dto)
        {
            try
            {
                if (dto.CountryOfBirthCode == "MMR")
                {
                    if (string.IsNullOrWhiteSpace(dto.NRC) || string.IsNullOrWhiteSpace(dto.FatherName))
                    {
                        throw new ArgumentException("Need NRC and FatherName for Myanmar Citizens");
                    }
                }

                var traveller = _mapper.Map<Traveller>(dto);
                var arrivalApplication = _mapper.Map<ArrivalApplication>(dto);
                return await _repository.SubmitArrivalApplicationAsync(traveller, arrivalApplication);
            }
            catch (ArgumentException)
            {

                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SubmitAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<ResponseCompleteArrivalDTO?> GetScanAsync(Guid AppNo)
        {
            try
            {
                var app = await _repository.GetArrivalApplicationDetailsAsync(AppNo);
                if (app == null) return null;

                var result = _mapper.Map<ResponseCompleteArrivalDTO>(app);

                if (app.Traveller != null) _mapper.Map(app.Traveller, result);
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
            catch (Exception ex)
            {
                Console.WriteLine($"Could not find or map {AppNo}. Exception: {ex.Message}");
                throw;
            }
        }
    }
}