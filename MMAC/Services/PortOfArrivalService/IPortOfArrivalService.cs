using MMAC.Models.Master;

namespace MMAC.Services.PortOfArrivalService
{
    public interface IPortOfArrivalService
    {
        Task<IEnumerable<PortOfArrival>> GetPortsByModeOfTravelAsync(int modeOfTravelId);
    }
}