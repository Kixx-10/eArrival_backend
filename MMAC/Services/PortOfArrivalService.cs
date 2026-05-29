using MMAC.Models.Master;
using MMAC.Repositories;

namespace MMAC.Services
{
    public class PortOfArrivalService : IPortOfArrivalService
    {
        private readonly IPortOfArrivalRepository _repository;

        public PortOfArrivalService(IPortOfArrivalRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PortOfArrival>> GetPortsByModeOfTravelAsync(int modeOfTravelId)
        {
            try
            {
                return await _repository.GetPortOfArrivalAsync(modeOfTravelId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<PortOfArrival>();
            }
        }
    }
}