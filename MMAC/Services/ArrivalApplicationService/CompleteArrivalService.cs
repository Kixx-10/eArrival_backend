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

        public async Task<ArrivalSubmitResponseDTO> SubmitAsync(CompleteArrivalDTO dto)
        {
            try
            {
                // Validate Myanmar Citizen
                if (dto.CountryOfBirthCode == "MMR")
                {
                    if (string.IsNullOrWhiteSpace(dto.NRC) || string.IsNullOrWhiteSpace(dto.FatherName))
                    {
                        throw new ArgumentException("Need NRC and FatherName for Myanmar Citizens");
                    }
                }

                // ၂။ AutoMapper Mapping (DTO to Entity)
                var traveller = _mapper.Map<Traveller>(dto);
                var arrivalApplication = _mapper.Map<ArrivalApplication>(dto);

                //  Business Logic for ReferenceNo
                if (!string.IsNullOrEmpty(dto.ReferenceNo))
                {
                    // If user updates, re-use old ReferenceNo
                    arrivalApplication.ReferenceNo = dto.ReferenceNo;
                }
                else
                {
                    // If new user, generate a unique ReferenceNo (MMR-YYYY-XXXXXXXX)
                    string currentYear = DateTime.Now.Year.ToString();
                    string finalReferenceNo = string.Empty;
                    bool isDuplicate = true;

                    while (isDuplicate)
                    {
                        int randomNumber = Random.Shared.Next(10000000, 99999999);
                        finalReferenceNo = $"MMR-{currentYear}-{randomNumber}";

                        // Check uniqueness against database via repository
                        isDuplicate = await _repository.IsReferenceNoExistsAsync(finalReferenceNo);
                    }

                    arrivalApplication.ReferenceNo = finalReferenceNo;
                }

                //  Generate New Guid for QR Code and set status
                arrivalApplication.AppNo = Guid.NewGuid();
                arrivalApplication.AppStatus = "Active";

                //  Save to database via Repository (Returns the saved AppNo Guid)
                Guid savedAppNo = await _repository.SubmitArrivalApplicationAsync(traveller, arrivalApplication);

                //  Return both ApplicationNo and ReferenceNo wrapped in DTO
                return new ArrivalSubmitResponseDTO
                {
                    ApplicationNo = savedAppNo,
                    ReferenceNo = arrivalApplication.ReferenceNo
                };
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

                // if old qr invalid
                if (app.AppStatus == "Invalid")
                {
                    throw new InvalidOperationException("This QR Code is invalid because the application has been updated with a new version.");
                }

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
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not find or map {AppNo}. Exception: {ex.Message}");
                throw;
            }
        }
    }
}