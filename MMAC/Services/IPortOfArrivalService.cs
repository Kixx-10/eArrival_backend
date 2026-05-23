using MMAC.Models.Master;

namespace MMAC.Services
{
    public interface IPortOfArrivalService
    {
        Task<IEnumerable<PortOfArrival>> GetPortsByModeOfTravelAsync(int modeOfTravelId);
    }
}