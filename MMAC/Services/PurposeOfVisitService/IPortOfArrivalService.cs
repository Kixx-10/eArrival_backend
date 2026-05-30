using MMAC.Models.Master;

namespace MMAC.Services.PurposeOfVisitService.PurposeOfVisitService
{
    public interface IPortOfArrivalService
    {
        Task<IEnumerable<PortOfArrival>> GetPortsByModeOfTravelAsync(int modeOfTravelId);
    }
}