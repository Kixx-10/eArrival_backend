using Microsoft.EntityFrameworkCore;
using MMAC.Data;
using MMAC.Models.Master;

namespace MMAC.Repositories
{
    public class PortOfArrivalRepository : IPortOfArrivalRepository
    {
        private readonly AppDbContext _context;

        public PortOfArrivalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PortOfArrival>> GetPortOfArrivalAsync(int ModeOfTravelId)
        {
            try
            {

                return await _context.PortOfArrival
                    .Include(p => p.ModeOfTravel)
                    .Where(p => p.ModeOfTravelId == ModeOfTravelId)
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