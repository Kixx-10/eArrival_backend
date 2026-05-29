using MMAC.DTOS;

namespace MMAC.Services.UtilityService
{
    public interface IUtilityService
    {
        Task<IEnumerable<LocationDTO>> GetLocations();
    }
}
