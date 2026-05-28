using MMAC.Models.Master;

namespace MMAC.Repositories
{
    public interface IPortOfArrivalRepository
    {
        Task<IEnumerable<PortOfArrival>> GetPortOfArrivalAsync(int ModeOfTravelId);
    }
}
