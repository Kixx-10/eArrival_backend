using Microsoft.EntityFrameworkCore;
using MMAC.Data;
using MMAC.Models.Master;

namespace MMAC.Services
{
    public class PortOfArrivalService : IPortOfArrivalService
    {
        private readonly AppDbContext _context;

        public PortOfArrivalService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PortOfArrival>> GetPortsByModeOfTravelAsync(int modeOfTravelId)
        {
            try
            {
                return await _context.PortOfArrival
                                     .Where(p => p.ModeOfTravelId == modeOfTravelId)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new List<PortOfArrival>();
            }
        }
    }
}